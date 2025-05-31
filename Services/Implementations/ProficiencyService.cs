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
        public async Task<IEnumerable<Proficiency>> GetProficiencyAsync()
        {
            return await _context.Proficiencies.ToListAsync();  
        }
        // add
        public async Task<ProficiencyDto> CreateProficiencyAsync(CreateProficiencyDto dto)
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

            return new ProficiencyDto
            {
                Id = entity.Id,
                Band = entity.Band,
                Name = entity.Name,
                Description = entity.Description
            };
        }
        public async Task<List<ProficiencyDto>> GetAllAsync()
        {
            return await _context.Proficiencies
                .Select(p => new ProficiencyDto
                {
                    Id = p.Id,
                    Band = p.Band,
                    Name = p.Name,
                    Description = p.Description
                }).ToListAsync();
        }



    }
}
