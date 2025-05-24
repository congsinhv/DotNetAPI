using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListeningResultDetailController : ControllerBase
{
    private readonly IListeningResultDetailService _listeningResultDetailService;

    public ListeningResultDetailController(IListeningResultDetailService listeningResultDetailService)
    {
        _listeningResultDetailService = listeningResultDetailService;
    }

    [HttpPost]
    public async Task<ActionResult<ListeningResultDetailsDto>> CreateListeningResultDetail([FromRoute] Guid listeningResultId, ListeningResultDetailCreateDto createContent)
    {
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }
        try
        {
            var createdResultDetail = await _listeningResultDetailService.CreateListeningResultDetailAsync(listeningResultId, createContent);
            if (createdResultDetail == null)
            {
                return NotFound("Listening result not found.");
            }
            return createdResultDetail;
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

    [HttpGet("{resultId}")]
    public async Task<ActionResult<List<ListeningResultDetailsDto>>> GetListeningResultDetailsByResultId(Guid resultId)
    {
        var existResult = await _listeningResultDetailService.GetListeningResultDetailsByResultIdAsync(resultId);
        if (resultId == Guid.Empty)
        {
            return BadRequest("Invalid result ID.");
        }
        if (existResult == null)
        {
            return NotFound("Listening result details not found.");
        }
        try
        {
            var details = await _listeningResultDetailService.GetListeningResultDetailsByResultIdAsync(resultId);
            if (details == null || !details.Any())
            {
                return NotFound("No details found for the given result ID.");
            }
            return Ok(details);
        }
        catch (Exception ex)
        {
            // Log exception (not shown here)
            return StatusCode(500, $"An error occurred while retrieving the details: {ex.Message}");
        }
    }
}