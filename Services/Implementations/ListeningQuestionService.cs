using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ListeningQuestionService : IListeningQuestionService
{
    private readonly ApplicationDbContext _context;
    private readonly IListeningAnswerService _listeningAnswerService;
    private readonly ITypesOfQuestionService _typesOfQuestionService;

    public ListeningQuestionService(
        ApplicationDbContext context,
        IListeningAnswerService listeningAnswerService,
        ITypesOfQuestionService typesOfQuestionService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _listeningAnswerService = listeningAnswerService ?? throw new ArgumentNullException(nameof(listeningAnswerService));
        _typesOfQuestionService = typesOfQuestionService ?? throw new ArgumentNullException(nameof(typesOfQuestionService));
    }

    public async Task<ListeningQuestionNoAnswerDto> CreateListeningQuestionAsync(ListeningQuestionCreate createContent)
    {
        if (createContent == null)
            throw new ArgumentNullException(nameof(createContent));

        var newQuestion = new ListeningQuestion
        {
            Question = createContent.Question,
            AudioUrl = createContent.AudioUrl,
            Script = createContent.Script,
            ImageUrl = createContent.ImageUrl,
            TypeOfQuestionId = createContent.TypeOfQuestionId,
            ExamId = createContent.ExamId
        };

        _context.ListeningQuestions.Add(newQuestion);
        await _context.SaveChangesAsync();

        var questionId = newQuestion.Id;

        // Create options for the new question
        var createdOptions = new List<ListeningAnswerResponseDto>();
        foreach (var option in createContent.options)
        {
            try
            {
                await _listeningAnswerService.CreateListeningAnswerAsync(questionId.ToString(), option);
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Failed to create answer option: {ex.Message}", ex);
            }
        }

        // Get the options for the new question
        var options = await _listeningAnswerService.GetListeningAnswersByQuestionIdAsync(questionId);
        if (options == null)
            throw new Exception("Insert failed: Could not retrieve created options.");

        // Get the type of question
        var typeOfQuestion = await _typesOfQuestionService.GetTypeByIdAsync(createContent.TypeOfQuestionId);
        if (typeOfQuestion == null)
            throw new Exception("Type of question not found.");

        return new ListeningQuestionNoAnswerDto
        {
            Id = newQuestion.Id,
            Question = newQuestion.Question,
            AudioUrl = newQuestion.AudioUrl,
            Script = newQuestion.Script,
            ImageUrl = newQuestion.ImageUrl,
            Type = typeOfQuestion,
            options = options,
        };
    }

    public async Task<IEnumerable<ListeningQuestionNoAnswerDto>> GetAllListeningQuestionsAsync(string examId)
    {
        if (string.IsNullOrEmpty(examId))
            throw new ArgumentNullException(nameof(examId));

        var questions = await _context.ListeningQuestions
            .Where(q => q.ExamId.ToString() == examId)
            .ToListAsync();

        var questionDtos = new List<ListeningQuestionNoAnswerDto>();

        foreach (var question in questions)
        {
            var options = await _listeningAnswerService.GetListeningAnswersByQuestionIdAsync(question.Id);
            var typeOfQuestion = await _typesOfQuestionService.GetTypeByIdAsync(question.TypeOfQuestionId);

            questionDtos.Add(new ListeningQuestionNoAnswerDto
            {
                Id = question.Id,
                Question = question.Question,
                AudioUrl = question.AudioUrl,
                Script = question.Script,
                ImageUrl = question.ImageUrl,
                Type = typeOfQuestion,
                options = options,
            });
        }

        return questionDtos;
    }
}