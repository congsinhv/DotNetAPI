using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProficiencyController : ControllerBase
{
    private readonly IProficiencyService _proficiencyService;

    public ProficiencyController(IProficiencyService proficiencyService)
    {
        _proficiencyService = proficiencyService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProficiencyDto>>> GetAllProficiencies()
    {
        var proficiencies = await _proficiencyService.GetAllProficienciesAsync();
        if (proficiencies == null || !proficiencies.Any())
        {
            return NotFound("No proficiencies found.");
        }
        return Ok(proficiencies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProficiencyDto>> GetProficiencyById(Guid id)
    {
        var proficiency = await _proficiencyService.GetProficiencyByIdAsync(id);
        if (proficiency == null)
        {
            return NotFound($"Proficiency with ID {id} not found.");
        }
        return Ok(proficiency);
    }

    [HttpPost]
    public async Task<ActionResult<ProficiencyDto>> CreateProficiency([FromBody] ProficiencyCreateOrUpdateDto createContent)
    {
        var existingProficiency = await _proficiencyService.GetProficiencyByNameAsync(createContent.Name);
        if (existingProficiency != null)
        {
            return Conflict($"Proficiency with name {createContent.Name} already exists.");
        }
        
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }

        var createdProficiency = await _proficiencyService.CreateProficiencyAsync(createContent);
        return CreatedAtAction(nameof(GetAllProficiencies), new { id = createdProficiency.Id }, createdProficiency);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProficiencyDto>> UpdateProficiency(Guid id, [FromBody] ProficiencyCreateOrUpdateDto updateContent)
    {
        if (updateContent == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedProficiency = await _proficiencyService.UpdateProficiencyAsync(id, updateContent);
        if (updatedProficiency == null)
        {
            return NotFound();
        }

        return Ok(updatedProficiency);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProficiency(Guid id)
    {
        var result = await _proficiencyService.DeleteProficiencyAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return Ok("Proficiency deleted successfully.");
    }
}