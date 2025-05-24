using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningResultController : ControllerBase
{
    private readonly IListeningResultService _listeningResultService;

    public ListeningResultController(IListeningResultService listeningResultService)
    {
        _listeningResultService = listeningResultService;
    }

    [HttpPost]
    public async Task<ActionResult<ListeningResultDto>> CreateListeningResult([FromBody] ListeningResultCreateDto createContent)
    {
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            var createdResult = await _listeningResultService.CreateListeningResultAsync(createContent);
            if (createdResult == null)
            {
                return BadRequest("Failed to create listening result.");
            }
            return createdResult;
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, $"An error occurred while creating the result: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ListeningResultDto>> GetListeningResultById(Guid resultId)
    {
        if (resultId == Guid.Empty)
        {
            return BadRequest("Invalid ID.");
        }

        var result = await _listeningResultService.GetDetailResultByResultIdAsync(resultId);
        if (result == null)
        {
            return NotFound($"List of listening result detail with result ID {resultId} not found.");
        }

        return Ok(result);
    }
}