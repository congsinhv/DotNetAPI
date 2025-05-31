using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpPost]
    public async Task<ActionResult<QuestionNoAnswerDto>> CreateQuestion([FromBody] QuestionCreate createContent)
    {
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            var createdQuestion = await _questionService.CreateQuestionAsync(createContent);
            if (createdQuestion == null)
            {
                return BadRequest("Failed to create question.");
            }
            return createdQuestion;
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