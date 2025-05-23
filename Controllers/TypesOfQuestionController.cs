using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DotnetAPIProject.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TypesOfQuestionController : ControllerBase
{
    private readonly ITypesOfQuestionService _typesOfQuestionService;

    public TypesOfQuestionController(ITypesOfQuestionService typesOfQuestionService)
    {
        _typesOfQuestionService = typesOfQuestionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TypesOfQuestionDto>>> GetAllTypes()
    {
        var typesOfQuestions = await _typesOfQuestionService.GetAllTypesAsync();
        return Ok(typesOfQuestions);
    }

    [HttpPost]
    public async Task<ActionResult<TypesOfQuestionDto>> CreateType([FromBody] TypesOfQuestionCreateOrUpdateDto createContent)
    {
        if (createContent == null)
        {
            return BadRequest("Invalid data.");
        }

        var createdType = await _typesOfQuestionService.CreateTypeAsync(createContent);
        return CreatedAtAction(nameof(GetAllTypes), new { id = createdType.Id }, createdType);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<TypesOfQuestionDto>> UpdateType(Guid id, [FromBody] TypesOfQuestionCreateOrUpdateDto updateContent)
    {
        if (updateContent == null)
        {
            return BadRequest("Invalid data.");
        }

        var updatedType = await _typesOfQuestionService.UpdateTypeAsync(id, updateContent);
        if (updatedType == null)
        {
            return NotFound();
        }

        return Ok(updatedType);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteType(Guid id)
    {
        var result = await _typesOfQuestionService.DeleteTypeAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}