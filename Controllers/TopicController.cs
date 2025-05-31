using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DotnetAPIProject.Models.DTOs;


namespace DotnetAPIProject.Controllers
{
    public class TopicController : ControllerBase
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("Topic")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetTopics([FromQuery] Guid? ProficiencyId = null)
        {
            try
            {
                var topics = await _topicService.GetTopicsAsync(ProficiencyId);
                if (topics == null || !topics.Any())
                {
                    return NotFound("No topics found.");
                }
                return Ok(topics);
            }
            catch (Exception ex)
            {
                // Log the exception (bạn có thể dùng ILogger để ghi log)
                return StatusCode(500, "An error occurred while retrieving topics.");
            }
        }

    }
}
