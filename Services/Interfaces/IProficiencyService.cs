using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces;


public interface IProficiencyService
{
    Task<IEnumerable<ProficiencyDto>> GetAllProficienciesAsync();
    Task<ProficiencyDto> CreateProficiencyAsync(ProficiencyCreateOrUpdateDto createContent);
    Task<ProficiencyDto> UpdateProficiencyAsync(Guid id, ProficiencyCreateOrUpdateDto updateContent);
    Task<bool> DeleteProficiencyAsync(Guid id);
}