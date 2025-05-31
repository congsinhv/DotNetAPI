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
        private readonly ITopicService _topicService;
        private readonly IProficiencyService _proficiencyService;
        private readonly IQuestionService _questionService;

        public ExamService(ApplicationDbContext context, ITopicService topicService, IProficiencyService proficiencyService, IQuestionService questionService)
        {
            _context = context;
            _topicService = topicService;
            _proficiencyService = proficiencyService;
            _questionService = questionService;
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

    public async Task<ExamHaveAnswerResponseDto> GetDetailExamByIdAsync(Guid examId)
    {
        var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null)
            return null;

        // Fetch proficiency details
        // var proficiency = await _proficiencyService.GetProficiencyByIdAsync(exam.ProficiencyId);
        var topic = await _topicService.GetTopicByIdAsync(exam.TopicId);

        if (topic == null)
            return null;

        // Fetch questions related to the exam
        var questions = await _questionService.GetAllQuestionsByExamIdAsync(examId);

        return new ExamHaveAnswerResponseDto
        {
            Id = exam.Id,
            Name = exam.Name,
            Topic = topic,
            Time = exam.Time,
            Skill = exam.Skill,
            questions = questions.ToList()
        };
    }
       
    }
}