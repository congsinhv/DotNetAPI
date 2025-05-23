using DotnetAPIProject.Controllers;
using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class ExamService : IExamService
{
    private readonly ApplicationDbContext _context;

    private readonly IProficiencyService _proficiencyService;

    private readonly IListeningQuestionService _listeningQuestionService;

    public ExamService(
        ApplicationDbContext context,
        IProficiencyService proficiencyService,
        IListeningQuestionService listeningQuestionService)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _proficiencyService = proficiencyService ?? throw new ArgumentNullException(nameof(proficiencyService));
        _listeningQuestionService = listeningQuestionService ?? throw new ArgumentNullException(nameof(listeningQuestionService));
    }

    public async Task<IEnumerable<ExamBaseDto>> GetExamsByNameAsync(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Enumerable.Empty<ExamBaseDto>();

        var exams = await _context.Exams
            .Where(e => e.Name.Contains(name))
            .AsNoTracking()
            .ToListAsync();

        // For each exam, fetch its proficiency
        var proficiencyIds = exams.Select(e => e.ProficiencyId).Distinct().ToList();
        var proficiencies = await _context.Proficiencies
            .Where(p => proficiencyIds.Contains(p.Id))
            .ToListAsync();

        return exams.Select(e => new ExamBaseDto
        {
            Id = e.Id,
            Name = e.Name,
            Topic = e.Topic ?? string.Empty,
            Time = e.Time,
            Skill = e.Skill,
            Proficiency = proficiencies
                .Where(p => p.Id == e.ProficiencyId)
                .Select(p => new ProficiencyDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Band = p.Band,
                    Description = p.Description ?? string.Empty,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefault() ?? new ProficiencyDto()
        });
    }

    public async Task<ExamBaseDto> CreateExamAsync(ExamCreate exam)
    {
        if (exam == null)
            throw new ArgumentNullException(nameof(exam));

        var entity = new Exam
        {
            Id = Guid.NewGuid(),
            Name = exam.Name,
            Topic = exam.Topic,
            Time = exam.Time,
            Skill = exam.Skill,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            ProficiencyId = exam.ProficiencyId
        };
        _context.Exams.Add(entity);
        await _context.SaveChangesAsync();

        var proficiency = await _context.Proficiencies
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == exam.ProficiencyId);

        if (proficiency == null)
            throw new Exception("Proficiency not found.");

        return new ExamBaseDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Topic = entity.Topic ?? string.Empty,
            Time = entity.Time,
            Skill = entity.Skill,
            Proficiency = new ProficiencyDto
            {
                Id = proficiency.Id,
                Name = proficiency.Name,
                Band = proficiency.Band,
                Description = proficiency.Description ?? string.Empty,
                CreatedAt = proficiency.CreatedAt,
                UpdatedAt = proficiency.UpdatedAt
            }
        };
    }

    public async Task<ExamNoAnswerDto?> GetExamByIdAsync(Guid examId)
    {
        var exam = await _context.Exams.FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null)
            return null;

        // Fetch proficiency details
        var proficiency = await _proficiencyService.GetProficiencyByIdAsync(exam.ProficiencyId);

        if (proficiency == null)
            return null;

        // Fetch questions related to the exam
        var questions = await _listeningQuestionService.GetAllListeningQuestionsAsync(examId.ToString());

        return new ExamNoAnswerDto
        {
            Id = exam.Id,
            Name = exam.Name,
            Topic = exam.Topic ?? string.Empty,
            Time = exam.Time,
            Skill = exam.Skill,
            CreatedAt = exam.CreatedAt,
            UpdatedAt = exam.UpdatedAt,
            Proficiency = new ProficiencyDto
            {
                Id = proficiency.Id,
                Name = proficiency.Name,
                Band = proficiency.Band,
                Description = proficiency.Description ?? string.Empty,
                CreatedAt = proficiency.CreatedAt,
                UpdatedAt = proficiency.UpdatedAt
            },
            questions = questions.ToList()
        };
    }


    public async Task<bool> DeleteExamAsync(Guid examId)
    {
        var exam = await _context.Exams.FindAsync(examId);
        if (exam == null)
            return false;
        _context.Exams.Remove(exam);
        await _context.SaveChangesAsync();
        return true;
    }
}