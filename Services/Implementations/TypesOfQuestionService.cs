using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class TypesOfQuestionService : ITypesOfQuestionService
{
    private readonly ApplicationDbContext _context;

    public TypesOfQuestionService(ApplicationDbContext context)
    {
        _context = context;
    }

    //Get all types of questions
    public async Task<IEnumerable<TypesOfQuestionDto>> GetAllTypesAsync()
    {
        var types = await _context.TypesOfQuestions.ToListAsync();
        return types.Select(t => new TypesOfQuestionDto
        {
            Id = t.Id,
            Name = t.Name,
            Description = t.Description ?? string.Empty,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });
    }
    //Create a new type of question
    public async Task<TypesOfQuestionDto> CreateTypeAsync(TypesOfQuestionCreateOrUpdateDto createContent)
    {
        var type = new TypesOfQuestion
        {
            Name = createContent.Name ?? string.Empty,
            Description = createContent.Description,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        _context.TypesOfQuestions.Add(type);
        await _context.SaveChangesAsync();

        return new TypesOfQuestionDto
        {
            Id = type.Id,
            Name = type.Name ?? string.Empty,
            Description = type.Description ?? string.Empty,
            CreatedAt = type.CreatedAt,
            UpdatedAt = type.UpdatedAt
        };
    }
    //Update an existing type of question
    public async Task<TypesOfQuestionDto> UpdateTypeAsync(Guid id, TypesOfQuestionCreateOrUpdateDto updateContent)
    {
        var type = await _context.TypesOfQuestions.FindAsync(id);
        if (type == null)
        {
            return null;
        }

        type.Name = updateContent.Name ?? type.Name;
        type.Description = updateContent.Description;
        type.UpdatedAt = DateTime.Now;

        _context.TypesOfQuestions.Update(type);
        await _context.SaveChangesAsync();

        return new TypesOfQuestionDto
        {
            Id = type.Id,
            Name = type.Name ?? string.Empty,
            Description = type.Description ?? string.Empty,
            CreatedAt = type.CreatedAt,
            UpdatedAt = type.UpdatedAt
        };
    }

    //Delete a type of question
    public async Task<bool> DeleteTypeAsync(Guid id)
    {
        var type = await _context.TypesOfQuestions.FindAsync(id);
        if (type == null)
        {
            return false;
        }

        _context.TypesOfQuestions.Remove(type);
        await _context.SaveChangesAsync();
        return true;
    }
}