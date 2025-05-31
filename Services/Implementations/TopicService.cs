using DotnetAPIProject.Data;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using DotnetAPIProject.Models.DTOs;

using Microsoft.EntityFrameworkCore;


namespace DotnetAPIProject.Services.Implementations
{
    public class TopicService : ITopicService
    {
        private readonly ApplicationDbContext _context;

        public TopicService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TopicDto>> GetTopicsAsync(Guid? ProficiencyId)
        {
            var query = _context.Topics
                .Include(t => t.Proficiency)
                .Select(t => new TopicDto
                {
                    IdTopic = t.Id,
                    Name = t.Name,
                    IdProficiency = t.ProficienciesId,
                    NameProficiency = t.Proficiency.Name
                });

            if (ProficiencyId.HasValue)
            {
                query = query.Where(t => t.IdProficiency == ProficiencyId.Value);
            }

            return await query.ToListAsync();
        }
        // add 
        public async Task<TopicDto> CreateTopicAsync(CreateTopicDto dto)
        {
            var entity = new Topic
            {
                Id = Guid.NewGuid(),
                Name = dto.TopicName,
                ProficienciesId = dto.ProficiencyId
            };

            _context.Topics.Add(entity);
            await _context.SaveChangesAsync();

            var proficiency = await _context.Proficiencies.FindAsync(dto.ProficiencyId);

            return new TopicDto
            {
                IdTopic = entity.Id,
                Name = entity.Name,
                IdProficiency = entity.ProficienciesId,
                NameProficiency = proficiency?.Name
            };
        }





    }
}
