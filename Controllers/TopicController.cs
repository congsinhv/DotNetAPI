using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DotnetAPIProject.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;


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
        public async Task<ActionResult<IEnumerable<TopicResponseDto>>> GetTopics([FromQuery] Guid ProficiencyId)
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
 
        // add 
        [HttpPost("Topic")]
        public async Task<IActionResult> Create([FromBody] CreateTopicDto dto)
        {
            var result = await _topicService.CreateTopicAsync(dto);
            return CreatedAtAction(nameof(GetTopicByIdAsync), new { idTopic = result.IdTopic }, result);

        }

        [HttpGet("Topic/{topicId}")]
        public async Task<ActionResult<TopicDto>> GetTopicByIdAsync([FromRoute] Guid topicId)
        {
            var topic = await _topicService.GetTopicByIdAsync(topicId);
            if (topic == null)
                return NotFound("Topic not found.");
            return Ok(topic);
        }
    }
}
