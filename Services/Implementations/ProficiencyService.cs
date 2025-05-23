using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ProficiencyService : IProficiencyService
{
    private readonly ApplicationDbContext _context;

    public ProficiencyService(ApplicationDbContext context)
    {
        _context = context;
    }

    //Get all proficiencies
    public async Task<IEnumerable<ProficiencyDto>> GetAllProficienciesAsync()
    {
        var proficiencies = await _context.Proficiencies.ToListAsync();
        return proficiencies.Select(p => new ProficiencyDto
        {
            Id = p.Id,
            Band = p.Band ?? string.Empty,
            Name = p.Name,
            Description = p.Description ?? string.Empty,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt
        });
    }
        //Create a new proficiency
        public async Task<ProficiencyDto> CreateProficiencyAsync(ProficiencyCreateOrUpdateDto createContent)
        {
            var proficiency = new Proficiency
            {
                Band = createContent.Band ?? string.Empty,
                Name = createContent.Name ?? string.Empty,
                Description = createContent.Description ?? string.Empty,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Proficiencies.Add(proficiency);
            await _context.SaveChangesAsync();

            return new ProficiencyDto
            {
                Id = proficiency.Id,
                Band = proficiency.Band ?? string.Empty,
                Name = proficiency.Name ?? string.Empty,
                Description = proficiency.Description ?? string.Empty,
                CreatedAt = proficiency.CreatedAt,
                UpdatedAt = proficiency.UpdatedAt
            };
        }
        //Update an existing proficiency
        public async Task<ProficiencyDto> UpdateProficiencyAsync(Guid id, ProficiencyCreateOrUpdateDto updateContent)
        {
            var proficiency = await _context.Proficiencies.FindAsync(id);
            if (proficiency == null)
            {
                return null;
            }

            proficiency.Band = updateContent.Band ?? proficiency.Band;
            proficiency.Name = updateContent.Name ?? proficiency.Name;
            proficiency.Description = updateContent.Description ?? proficiency.Description;
            proficiency.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return new ProficiencyDto
            {
                Id = proficiency.Id,
                Band = proficiency.Band ?? string.Empty,
                Name = proficiency.Name ?? string.Empty,
                Description = proficiency.Description ?? string.Empty,
                CreatedAt = proficiency.CreatedAt,
                UpdatedAt = proficiency.UpdatedAt
            };
        }
        //Delete a proficiency
        public async Task<bool> DeleteProficiencyAsync(Guid id)
        {
            var proficiency = await _context.Proficiencies.FindAsync(id);
            if (proficiency == null)
            {
                return false;
            }

            _context.Proficiencies.Remove(proficiency);
            await _context.SaveChangesAsync();
            return true;
        }
}