using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace DotnetAPIProject.Services.Implementations
{
    public class ExamService : IExamsService
    {
        private readonly ApplicationDbContext _context;

        public ExamService(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public async Task<IEnumerable<ExamDto>> GetExamAsync(Guid? topicId)
        {
            var query = _context.Exams
                .Include(qs => qs.Topic) //
                .Select(qs => new ExamDto
                {
                    IdExam = qs.Id,
                    NameExam = qs.Name,
                    TopicID = qs.TopicId,
                    TopicName = qs.Topic.Name,
               
                });

            if (topicId.HasValue)
            {
                query = query.Where(qs => qs.TopicID == topicId.Value);
            }


            return await query.ToListAsync();
        }
        //
     
       
    }
}