using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class QuestionService : IQuestionService
{
    private readonly ApplicationDbContext _context;
    private readonly IAnswerService _answerService;

    public QuestionService(
        ApplicationDbContext context,
        IAnswerService answerService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _answerService = answerService ?? throw new ArgumentNullException(nameof(answerService));
    }

    public async Task<QuestionNoAnswerDto> CreateQuestionAsync(QuestionCreate createContent)
    {
        if (createContent == null)
            throw new ArgumentNullException(nameof(createContent));

        var newQuestion = new Question
        {
            Content = createContent.Content,
            ExamId = createContent.ExamId,
            TypeQuestion = createContent.TypeQuestion
        };

        _context.Questions.Add(newQuestion);
        await _context.SaveChangesAsync();

        var questionId = newQuestion.Id;

        // Create options for the new question
        var createdOptions = new List<AnswerResponseDto>();
        foreach (var option in createContent.Options)
        {
            try
            {
                await _answerService.CreateAnswerAsync(questionId, option);
            }
            catch (Exception ex)
            {
                // Optionally log the error
                throw new Exception($"Failed to create answer option: {ex.Message}", ex);
            }
        }

        // Get the options for the new question
        var options = await _answerService.GetAnswersByQuestionIdAsync(questionId);
        if (options == null)
            throw new Exception("Insert failed: Could not retrieve created options.");

        return new QuestionNoAnswerDto
        {
            Id = newQuestion.Id,
            Content = newQuestion.Content,
            TypeQuestion = newQuestion.TypeQuestion
        };
    }

    public async Task<IEnumerable<QuestionNoAnswerDto>> GetAllQuestionsByExamIdAsync(Guid examId)
    {
        if (examId == Guid.Empty)
            throw new ArgumentNullException(nameof(examId));

        var questions = await _context.Questions
            .Where(q => q.ExamId == examId)
            .ToListAsync();

        var questionDtos = new List<QuestionNoAnswerDto>();

        foreach (var question in questions)
        {
            var options = await _answerService.GetAnswersByQuestionIdAsync(question.Id);

            questionDtos.Add(new QuestionNoAnswerDto
            {
                Id = question.Id,
                Content = question.Content,
                TypeQuestion = question.TypeQuestion
            });
        }

        return questionDtos;
    }
}