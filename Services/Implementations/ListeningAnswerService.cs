using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ListeningAnswerService : IListeningAnswerService
{
    private readonly ApplicationDbContext _context;

    public ListeningAnswerService(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public bool IsCorrectAnswer(Guid answerId)
    {
        if (answerId == Guid.Empty)
            return false;
        var answer = _context.ListeningAnswers
            .AsNoTracking()
            .FirstOrDefault(a => a.Id == answerId);

        return answer?.IsCorrect ?? false;
    }
    
    public async Task<Models.DTOs.ListeningAnswer> CreateListeningAnswerAsync(string questionId, ListeningAnswerCreate listeningAnswerCreate)
    {
        if (!Guid.TryParse(questionId, out var questionGuid))
            throw new ArgumentException("Invalid questionId format.", nameof(questionId));

        var entity = new Models.Entities.ListeningAnswer
        {
            Id = Guid.NewGuid(),
            symbol = listeningAnswerCreate.symbol ?? string.Empty,
            description = listeningAnswerCreate.description ?? string.Empty,
            IsCorrect = listeningAnswerCreate.IsCorrect ?? false,
            ListeningQuestionId = questionGuid
        };
        _context.ListeningAnswers.Add(entity);
        await _context.SaveChangesAsync();
        return new Models.DTOs.ListeningAnswer
        {
            Id = entity.Id,
            symbol = entity.symbol,
            description = entity.description,
            IsCorrect = entity.IsCorrect
        };
    }

    public async Task<List<ListeningAnswerResponseDto>> GetListeningAnswersByQuestionIdAsync(Guid questionId)
    {
        var answers = await _context.ListeningAnswers
            .AsNoTracking()
            .Where(x => x.ListeningQuestionId == questionId)
            .Select(x => new ListeningAnswerResponseDto
            {
                Id = x.Id,
                symbol = x.symbol,
                description = x.description
            })
            .ToListAsync();
        return answers;
    }

    public async Task<bool> DeleteListeningAnswerAsync(Guid id)
    {
        var entity = await _context.ListeningAnswers.FindAsync(id);
        if (entity == null)
            return false;
        _context.ListeningAnswers.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Models.DTOs.ListeningAnswer> UpdateListeningAnswerAsync(Guid id, ListeningAnswerUpdate update)
    {
        var entity = await _context.ListeningAnswers.FindAsync(id);
        if (entity == null)
            throw new Exception("ListeningAnswer not found.");

        if (update.symbol != null)
            entity.symbol = update.symbol;
        if (update.description != null)
            entity.description = update.description;
        if (update.IsCorrect.HasValue)
            entity.IsCorrect = update.IsCorrect.Value;

        await _context.SaveChangesAsync();

        return new Models.DTOs.ListeningAnswer
        {
            Id = entity.Id,
            symbol = entity.symbol,
            description = entity.description,
            IsCorrect = entity.IsCorrect
        };
    }
}