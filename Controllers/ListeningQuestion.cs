using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningQuestionController : ControllerBase
{
    private readonly IListeningQuestionService _listeningQuestionService;

    public ListeningQuestionController(IListeningQuestionService listeningQuestionService)
    {
        _listeningQuestionService = listeningQuestionService;
    }

    /// <summary>
    /// Creates a new listening question and its answers.
    /// </summary>
    /// <param name="createContent">The question and options to create.</param>
    /// <returns>The created question DTO without answers.</returns>
    [HttpPost]
    public async Task<ActionResult<ListeningQuestionNoAnswerDto>> CreateListeningQuestion([FromBody] ListeningQuestionCreate createContent)
    {
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            var createdQuestion = await _listeningQuestionService.CreateListeningQuestionAsync(createContent);
            return CreatedAtAction(nameof(CreateListeningQuestion), new { id = createdQuestion.Id }, createdQuestion);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, $"An error occurred while creating the question: {ex.Message}");
        }
    }
}