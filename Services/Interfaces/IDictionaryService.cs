using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces;

public interface IDictionaryService
{
    Task<DictionaryItem> CreateAsync(DictionaryItemDto item);
    Task<IEnumerable<DictionaryItem>> GetAllAsync();
    Task<DictionaryItem?> GetByIdAsync(int id);
    Task<DictionaryItem?> UpdateAsync(int id, DictionaryItemDto item);
    Task<bool> DeleteAsync(int id);
    Task<string> GetWordDefinitionAsync(string word);
}
