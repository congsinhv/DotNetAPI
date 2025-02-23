using System.Net.Http;
using System.Text.Json;
using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class DictionaryService : IDictionaryService
{
    private readonly ApplicationDbContext _context;
    private readonly string _baseUrl;
    private readonly IHttpClientFactory _httpClientFactory;

    public DictionaryService(ApplicationDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
        _baseUrl = Environment.GetEnvironmentVariable("OXFORD_DICTIONARY_BASE_URL") ?? "dummy";
    }

    public async Task<DictionaryItem> CreateAsync(DictionaryItemDto item)
    {
        var dictionaryItem = new DictionaryItem
        {
            Word = item.Word,
            Definition = item.Definition,
            WorkspaceId = item.WorkspaceId,
        };

        _context.DictionaryItems.Add(dictionaryItem);
        await _context.SaveChangesAsync();
        return dictionaryItem;
    }

    public async Task<IEnumerable<DictionaryItem>> GetAllAsync()
    {
        return await _context.DictionaryItems.ToListAsync();
    }

    public async Task<DictionaryItem?> GetByIdAsync(int id)
    {
        return await _context.DictionaryItems.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<DictionaryItem?> UpdateAsync(int id, DictionaryItemDto item)
    {
        var existingItem = await _context.DictionaryItems.FindAsync(id);

        if (existingItem == null)
            return null;

        existingItem.Word = item.Word;
        existingItem.Definition = item.Definition;
        existingItem.WorkspaceId = item.WorkspaceId;

        await _context.SaveChangesAsync();
        return existingItem;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.DictionaryItems.FindAsync(id);
        if (item == null)
            return false;

        _context.DictionaryItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string> GetWordDefinitionAsync(string word)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var url = $"{_baseUrl}/entries/en/{word.ToLower()}";

        var response = await httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Error calling Oxford API: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        return doc.RootElement.ToString();
    }
}
