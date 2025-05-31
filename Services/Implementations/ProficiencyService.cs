using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DotnetAPIProject.Services.Implementations
{
    public class ProficiencyService : IProficiencyService
    {
        private readonly ApplicationDbContext _context;

        public ProficiencyService(ApplicationDbContext context)
        {
            _context = context;
        }
        // add
        public async Task<ProficiencyResponseDto> CreateProficiencyAsync(CreateProficiencyDto dto)
        {
            var entity = new Proficiency
            {
                Id = Guid.NewGuid(),
                Band = dto.Band,
                Name = dto.Name,
                Description = dto.Description
            };

            _context.Proficiencies.Add(entity);
            await _context.SaveChangesAsync();

            return new ProficiencyResponseDto
            {
                Id = entity.Id,
                Band = entity.Band,
                Name = entity.Name,
                Description = entity.Description
            };
        }
        public async Task<List<ProficiencyResponseDto>> GetAllAsync()
        {
            // Get all proficiencies and map to ProficiencyResponseDto
            var proficiencyList = await _context.Proficiencies.ToListAsync();
            if (proficiencyList == null || !proficiencyList.Any())
            {
                return new List<ProficiencyResponseDto>();
            }
            
            return await _context.Proficiencies
                .Select(p => new ProficiencyResponseDto
                {
                    Id = p.Id,
                    Band = p.Band,
                    Name = p.Name,
                    Description = p.Description
                }).ToListAsync();
        }

        public async Task<Proficiency> GetProficiencyByIdAsync(Guid proficiencyId)
        {
            var proficiency = await _context.Proficiencies.FindAsync(proficiencyId);
            if (proficiency == null)
            {
                throw new Exception("Proficiency not found");
            }
            return proficiency;
        }
    }
}
