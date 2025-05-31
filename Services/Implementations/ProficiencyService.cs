using DotnetAPIProject.Data;
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
            return await _context.Proficiencies.ToListAsync();  // Lấy tất cả các Level từ DB
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
