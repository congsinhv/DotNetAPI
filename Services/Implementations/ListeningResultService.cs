using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ListeningResultService : IListeningResultService
{
    private readonly ApplicationDbContext _context;
    private readonly IListeningResultDetailService _listeningResultDetailService;
    private readonly ListeningAnswerService _listeningAnswerService;

    public ListeningResultService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _listeningResultDetailService = new ListeningResultDetailService(context);
        _listeningAnswerService = new ListeningAnswerService(context);
    }

    public async Task<ListeningResultDto> GetListeningResultByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var resultId))
            return null;

        var result = await _context
            .ListeningResults.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == resultId);

        if (result == null)
            return null;

        return new ListeningResultDto
        {
            Id = result.Id,
            UserId = result.UserId,
            ExamId = result.ExamId,
            FinishTime = result.FinishTime,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
        };
    }

    public async Task<ListeningResultDto> CreateListeningResultAsync(
        ListeningResultCreateDto createContent
    )
    {
        if (createContent == null)
            throw new ArgumentNullException(nameof(createContent));

        var newResult = new ListeningResult
        {
            UserId = createContent.UserId,
            ExamId = createContent.ExamId,
            FinishTime = createContent.FinishTime,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };

        // 1. Add and save the ListeningResult first
        _context.ListeningResults.Add(newResult);
        await _context.SaveChangesAsync(); // newResult.Id is now valid

        // 2. Now add ListeningResultDetails
        foreach (var answer in createContent.UserAnswers)
        {
            await _listeningResultDetailService.CreateListeningResultDetailAsync(
                newResult.Id,
                answer
            );
        }

        // 3. Update the status of ResultDetail
        // Update status of each item in userAnswers = result of _listeningAnswerService.IsCorrectAnswer
        var userAnswers =
            await _listeningResultDetailService.GetListeningResultDetailsByResultIdAsync(
                newResult.Id
            );
        foreach (var answer in userAnswers)
        {
            var isCorrect = _listeningAnswerService.IsCorrectAnswer(answer.AnswerId ?? Guid.Empty);
            var status = new ListeningResultDetailUpdateDto
            {
                Status = isCorrect ? "correct" : "incorrect",
            };
            await _listeningResultDetailService.UpdateListeningResultDetailAsync(answer.Id, status);
        }

        await _context.SaveChangesAsync(); // newResult.Id is now valid
        var resultDto = new ListeningResultDto
        {
            Id = newResult.Id,
            UserId = newResult.UserId,
            ExamId = newResult.ExamId,
            FinishTime = newResult.FinishTime,
            CreatedAt = newResult.CreatedAt,
            UpdatedAt = newResult.UpdatedAt,
        };

        return resultDto;
    }

    public async Task<ResultResponseDto> GetDetailResultByResultIdAsync(Guid resultId)
    {
        var resultIdParam = new SqlParameter("@resultId", resultId);
        var sql =
            @"
        SELECT
            t_r.Id,
            t_e.Name as title,
            t_r.FinishTime,
            t_lq.Id as QuestionId,
            t_lq.Question,
            t_lq.AudioUrl,
            t_lq.Script,
            t_toq.Id AS TypeOfQuestionId,
            t_toq.Name AS TypeName,
            t_la.symbol,
            t_la.IsCorrect,
            t_la.description,
            t_la.Id AS AnswerId,
            CASE WHEN t_rd.Id IS NOT NULL THEN 1 ELSE 0 END AS IsSelected
        FROM ListeningAnswers t_la
        JOIN ListeningQuestions t_lq ON t_lq.Id = t_la.ListeningQuestionId
        JOIN TypesOfQuestions t_toq ON t_toq.Id = t_lq.TypeOfQuestionId
        JOIN Exams t_e ON t_e.Id = t_lq.ExamId
        JOIN ListeningResults t_r ON t_r.ExamId = t_e.Id
        LEFT JOIN ListeningResultDetails t_rd ON t_rd.AnswerId = t_la.Id AND t_rd.ListeningResultId = t_r.Id
        WHERE t_r.Id = @resultId
        ";

        var options = await _context
            .Database.SqlQueryRaw<ListeningOptionDto>(sql, resultIdParam)
            .ToListAsync();

        var overallScore = options.Sum(option =>
            option.IsCorrect && option.IsSelected == 1 ? 1 : 0
        );
        var id = options.FirstOrDefault()?.Id ?? Guid.Empty;
        var title = options.FirstOrDefault()?.Title ?? string.Empty;
        var finishedTime = options.FirstOrDefault()?.FinishTime ?? 0;

        // Group options by QuestionId to organize them by question
        var questionGroups = options.GroupBy(option => option.QuestionId);
        var results = new List<QuestionListening>();

        foreach (var group in questionGroups)
        {
            // Take the first option in the group to get question details
            var firstOption = group.First();

            results.Add(
                new QuestionListening
                {
                    Id = firstOption.QuestionId,
                    Question = firstOption.Question,
                    Audio = firstOption.AudioUrl,
                    Script = firstOption.Script,
                    Type = new TypesOfQuestion
                    {
                        Id = firstOption.TypeOfQuestionId,
                        Name = firstOption.TypeName,
                    },
                    // Map only the options that belong to this question
                    Options = group
                        .Select(option => new SelectedOption
                        {
                            Id = option.AnswerId,
                            Symbol = option.Symbol,
                            Description = option.Description,
                            IsSelected = option.IsSelected == 1,
                            IsCorrect = option.IsCorrect,
                        })
                        .ToList(),
                }
            );
        }

        var result = new ResultResponseDto
        {
            Id = id,
            Title = title,
            FinishedTime = finishedTime,
            OverallScore = overallScore,
            results = results,
        };

        return result;
    }
}
