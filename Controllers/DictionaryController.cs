using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Implementations;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DictionaryController : ControllerBase
{
    private readonly IDictionaryService _dictionaryService;

    public DictionaryController(IDictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }

    [HttpGet] 
    public async Task<ActionResult<IEnumerable<DictionaryItem>>> GetDictionary()
    {
        var dictionarys = await _dictionaryService.GetAllAsync();
        return Ok(dictionarys);
    }

    [HttpPost]

    public async Task<ActionResult<DictionaryItem>> CreateDictionary(DictionaryItemDto dictionaryDto)
    {
        var dictionnary = await _dictionaryService.CreateAsync(dictionaryDto);
        return CreatedAtAction(nameof(GetDictionary), new { id = dictionnary.Id }, dictionnary);
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<DictionaryItem>> UpdaUpdateAsync(int id, DictionaryItemDto dictionaryDto)
    {
        var dictionary = await _dictionaryService.UpdateAsync(id, dictionaryDto);
        return Ok(dictionary);
    }


    [HttpDelete("{id}")]

    public async Task<ActionResult<DictionaryItem>> DeleteAsync(int id)
    {
        var dictionary = await _dictionaryService.DeleteAsync(id);
        return Ok(dictionary);
    }


    // TODO: Implement the rest of the endpoints
    // GET /api/v1/dictionary/{id}
    // PUT /api/v1/dictionary/{id}
    // DELETE /api/v1/dictionary/{id}
}
