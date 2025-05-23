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

    public ExamController(IExamService examService)
    {
        _examService = examService;
    }

    [HttpPost]
    public async Task<ActionResult<ExamBaseDto>> CreateExam([FromBody] ExamCreate exam)
    {
        // Validate the incoming data
        var existingExam = await _examService.GetExamsByNameAsync(exam.Name);
        if (existingExam.Any())
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
}