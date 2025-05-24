using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController : ControllerBase
{
    private readonly IExamService _examService;

    private readonly IProficiencyService _proficiencyService;

    public ExamController(IExamService examService , IProficiencyService proficiencyService)
    {
        _examService = examService;
        _proficiencyService = proficiencyService;
    }

    [HttpPost]
    public async Task<ActionResult<ExamBaseDto>> CreateExam([FromBody] ExamCreate exam)
    {
        // Validate the incoming data
        var existingExam = await _examService.CheckExistExam(exam.Name, exam.ProficiencyId.ToString());
        if (existingExam != null)
        {
            return Conflict("An exam with this name already exists.");
        }
        if (exam == null)
        {
            return BadRequest("Invalid data.");
        }

        var createdExam = await _examService.CreateExamAsync(exam);

        if (createdExam == null)
        {
            return StatusCode(500, "An error occurred while creating the exam.");
        }

        return createdExam;
    }

    [HttpDelete("{examId}")]
    public async Task<ActionResult<bool>> DeleteExam([FromRoute] Guid examId)
    {
        var result = await _examService.DeleteExamAsync(examId);
        return result ? Ok() : NotFound();
    }

    [HttpGet("{examId}")]
    public async Task<ActionResult<ExamBaseDto>> GetExamById([FromRoute] Guid examId)
    {
        var exam = await _examService.GetExamByIdAsync(examId);
        if (exam == null)
        {
            return NotFound();
        }
        return Ok(exam);
    }

    [HttpGet("{proficiencyId}/exams")]
    public async Task<ActionResult<IEnumerable<ExamBaseDto>>> GetExamsByProficiencyId([FromRoute] Guid proficiencyId)
    {
        var existingProficiency = await _proficiencyService.GetProficiencyByIdAsync(proficiencyId);
        if (existingProficiency == null)
        {
            return NotFound("Proficiency not found.");
        }
        var exams = await _examService.GetExamsByProficiencyIdAsync(proficiencyId);
        if (exams == null || !exams.Any())
        {
            return Ok(new List<ExamBaseDto>());
        }
        return Ok(exams);
    }
}