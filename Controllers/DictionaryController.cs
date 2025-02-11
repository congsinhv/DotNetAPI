using Microsoft.AspNetCore.Mvc;
using DotnetAPIProject.Services.Interfaces;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

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

    [HttpPost]
    public async Task<string> CreateDictionaryItem(DictionaryItemDto itemDto)
    {
        var item = await _dictionaryService.CreateAsync(itemDto);
        return "Hello World";
    }

    [HttpGet]
    public string GET()
    {
        return "Hello World";
    }
    [HttpDelete]
    public string DELETE()
    {
        //  logic service
        // oke?
        return "Deleted";
    }
}

