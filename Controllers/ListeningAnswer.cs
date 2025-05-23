using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningAnswerController : ControllerBase
{
    private readonly IListeningAnswerService _listeningAnswerService;

    public ListeningAnswerController(IListeningAnswerService listeningAnswerService)
    {
        _listeningAnswerService = listeningAnswerService;
    }

    [HttpPost]
    [Route("{questionId}/answers")]
    public async Task<ActionResult<ListeningAnswer>> CreateListeningAnswer(
        [FromRoute] string questionId,
        [FromBody] ListeningAnswerCreate createContent)
    {
        if (string.IsNullOrWhiteSpace(questionId) || !Guid.TryParse(questionId, out _))
        {
            return BadRequest("Invalid questionId.");
        }
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }

        try
        {
            var createdAnswer = await _listeningAnswerService.CreateListeningAnswerAsync(questionId, createContent);
            return CreatedAtAction(nameof(CreateListeningAnswer), new { questionId = createdAnswer.Id }, createdAnswer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, $"An error occurred while creating the answer: {ex.Message}");
        }
    }
}