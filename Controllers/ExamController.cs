using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Services.Implementations;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController : ControllerBase
    {
        private readonly IExamsService _examService;

        public ExamController(IExamsService examService)
        {
            _examService = examService;
        }

        // GET: api/Exam?topicId={guid}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExamDto>>> GetExams([FromQuery] Guid? topicId = null)
        {
            try
            {
                var exams = await _examService.GetExamAsync(topicId);

                if (exams == null || !exams.Any())
                {
                    return NotFound("No exams found.");
                }

                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving exams.");
            }
        }
        // lấy
        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDto examDto)
        {
            try
            {
                var result = await _examService.CreateExamAsync(examDto);
                // return CreatedAtAction(nameof(GetExamById), new { id = result.IdExam }, result);
                return result != null ? CreatedAtAction(nameof(GetExamById), new { examId = result.IdExam }, result) : BadRequest("Failed to create exam.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the exam.");
            }
        }

       
        [HttpGet("{examId}")]
        public async Task<ActionResult<ExamHaveAnswerResponseDto>> GetExamById([FromRoute] Guid examId)
        {
            var exam = await _examService.GetDetailExamByIdAsync(examId);
            return Ok(exam);
        }
    }
}
