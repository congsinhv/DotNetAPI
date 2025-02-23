using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DotnetAPIProject.Models;
using DotnetAPIProject.Services.Implementations;

namespace DotnetAPIProject.Services.Interfaces;

public interface IDictionaryService
{
    Task<DictionaryItem> CreateAsync(DictionaryItemDto item);
    Task<IEnumerable<DictionaryItem>> GetAllAsync();
    Task<DictionaryItem?> GetByIdAsync(Guid id);
    Task<DictionaryItem?> UpdateAsync(Guid id, DictionaryItemDto item);
    Task<bool> DeleteAsync(Guid id);
    Task<string> GetWordDefinitionAsync(string word);
}
