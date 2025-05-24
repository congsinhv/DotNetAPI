using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
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

        var result = await _context.ListeningResults
            .AsNoTracking()
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
            UpdatedAt = result.UpdatedAt
        };
    }

 
    public async Task<ListeningResultDto> CreateListeningResultAsync(ListeningResultCreateDto createContent)
    {
        if (createContent == null)
            throw new ArgumentNullException(nameof(createContent));

        var newResult = new ListeningResult
        {
            UserId = createContent.UserId,
            ExamId = createContent.ExamId,
            FinishTime = createContent.FinishTime,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        // 1. Add and save the ListeningResult first
        _context.ListeningResults.Add(newResult);
        await _context.SaveChangesAsync(); // newResult.Id is now valid

        // 2. Now add ListeningResultDetails
        foreach (var answer in createContent.UserAnswers)
        {
            await _listeningResultDetailService.CreateListeningResultDetailAsync(newResult.Id, answer);
        }

        // 3. Update the status of ResultDetail
        // Update status of each item in userAnswers = result of _listeningAnswerService.IsCorrectAnswer
        var userAnswers = await _listeningResultDetailService.GetListeningResultDetailsByResultIdAsync(newResult.Id);
        foreach (var answer in userAnswers)
        {
            var isCorrect = _listeningAnswerService.IsCorrectAnswer(answer.AnswerId ?? Guid.Empty);
            var status = new ListeningResultDetailUpdateDto
            {
                Status = isCorrect ? "correct" : "incorrect"
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
            UpdatedAt = newResult.UpdatedAt
        };

        return resultDto;
    }

    public Task<ListeningResultHaveAnswerDto> GetDetailResultByResultIdAsync(Guid resultId)
    {

    
        // var 
        // return _context.ListeningResults
        //     .Where(x => x.Id == resultId)
        //     .Select(x => new ListeningResultHaveAnswerDto
        //     {
        //         Id = x.Id,
        //         UserId = x.UserId,
        //         ExamId = x.ExamId,
        //         FinishTime = x.FinishTime,
        //         OverallScore = x.OverallScore,
        //         CreatedAt = x.CreatedAt,
        //         UpdatedAt = x.UpdatedAt,
        //         results = _listeningResultDetailService.GetListeningResultDetailsByResultIdAsync(x.Id).Result
        //             .Select(detail => new ListeningQuestionHaveAnswerDto
        //             {
        //                 Id = detail.Id,
        //                 ListeningQuestionId = detail.ListeningQuestionId,
        //                 IsMarked = detail.IsMarked,
        //                 Status = detail.Status ?? string.Empty,
        //                 AnswerId = detail.AnswerId
        //             }).ToList()
        //     }).FirstOrDefaultAsync();
    }
}