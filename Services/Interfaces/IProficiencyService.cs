using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IProficiencyService
    {
        Task<IEnumerable<Proficiency>> GetProficiencyAsync();
        Task<ProficiencyDto> CreateProficiencyAsync(CreateProficiencyDto dto);
        Task<List<ProficiencyDto>> GetAllAsync();


    }
}
