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
      private readonly HttpClient _httpClient;
    private readonly ApplicationDbContext _context;
    private readonly string _baseUrl;
 

    public DictionaryService(ApplicationDbContext context, HttpClient httpClient)
    {
        _context = context;
        //_httpClientFactory = httpClientFactory;
        _httpClient = httpClient;
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

    public async Task<DictionaryItem?> GetByIdAsync(Guid id)
    {
        return await _context.DictionaryItems.FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<DictionaryItem?> UpdateAsync(Guid id, DictionaryItemDto item)
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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var item = await _context.DictionaryItems.FindAsync(id);
        if (item == null)
            return false;

        _context.DictionaryItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<DictionaryItemsDto> GetWordDefinitionAsync(string word)
    {
        var url = $"{_baseUrl}/entries/en/{word.ToLower()}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Dictionary API trả về lỗi: {response.StatusCode}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var jsonElements = JsonDocument.Parse(jsonResponse).RootElement;

        var result = new DictionaryItemsDto
        {
            Words = jsonElements[0].GetProperty("word").GetString(),
            Phonetic = jsonElements[0].TryGetProperty("phonetic", out var phonetic) ? phonetic.GetString() : "",
            Phonetics = new List<DictionaryItemsDto.PhoneticDTO>(),
            Meanings = new List<DictionaryItemsDto.MeaningDTO>()
        };

        if (jsonElements[0].TryGetProperty("phonetics", out var phonetics))
        {
            foreach (var item in phonetics.EnumerateArray())
            {
                result.Phonetics.Add(new DictionaryItemsDto.PhoneticDTO
                {
                    Text = item.TryGetProperty("text", out var text) ? text.GetString() : "",
                    Audio = item.TryGetProperty("audio", out var audio) ? audio.GetString() : ""
                });
            }
        }

        if (jsonElements[0].TryGetProperty("meanings", out var meanings))
        {
            foreach (var meaning in meanings.EnumerateArray())
            {
                var meaningDto = new DictionaryItemsDto.MeaningDTO
                {
                    PartOfSpeech = meaning.GetProperty("partOfSpeech").GetString(),
                    Definitions = new List<DictionaryItemsDto.DefinitionDTO>(),
                    Synonyms = new List<string>(),
                    Antonyms = new List<string>()
                };

                foreach (var definition in meaning.GetProperty("definitions").EnumerateArray())
                {
                    meaningDto.Definitions.Add(new DictionaryItemsDto.DefinitionDTO
                    {
                        Definitional = definition.GetProperty("definition").GetString(),
                        Example = definition.TryGetProperty("example", out var example) ? example.GetString() : "",
                        Synonyms = definition.TryGetProperty("synonyms", out var synonyms)
                            ? JsonSerializer.Deserialize<List<string>>(synonyms.GetRawText())
                            : new List<string>(),
                        Antonyms = definition.TryGetProperty("antonyms", out var antonyms)
                            ? JsonSerializer.Deserialize<List<string>>(antonyms.GetRawText())
                            : new List<string>()
                    });
                }

                meaningDto.Synonyms = meaning.TryGetProperty("synonyms", out var mainSynonyms)
                    ? JsonSerializer.Deserialize<List<string>>(mainSynonyms.GetRawText())
                    : new List<string>();

                meaningDto.Antonyms = meaning.TryGetProperty("antonyms", out var mainAntonyms)
                    ? JsonSerializer.Deserialize<List<string>>(mainAntonyms.GetRawText())
                    : new List<string>();

                result.Meanings.Add(meaningDto);
            }
        }

        return result;
    }
}
