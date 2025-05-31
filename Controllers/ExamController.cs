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
                // Log lỗi nếu có logger
                return StatusCode(500, "An error occurred while retrieving exams.");
            }
        }
    }
}
