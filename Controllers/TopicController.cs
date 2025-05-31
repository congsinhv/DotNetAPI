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
                return StatusCode(500, "An error occurred while retrieving topics.");
            }
        }
        // add 
        [HttpPost("Topic")]
        public async Task<IActionResult> Create([FromBody] CreateTopicDto dto)
        {
            var result = await _topicService.CreateTopicAsync(dto);
            return CreatedAtAction(nameof(GetById), new { idTopic = result.IdTopic }, result);

        }

        [HttpGet("{idTopic}")]
        public async Task<IActionResult> GetById(Guid idTopic)
        {
            var all = await _topicService.GetTopicsAsync(null); 
            var one = all.FirstOrDefault(x => x.IdTopic == idTopic);
            if (one == null) return NotFound();
            return Ok(one);
        }

    }
}
