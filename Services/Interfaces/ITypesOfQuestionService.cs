using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces;


public interface ITypesOfQuestionService
{
    Task<IEnumerable<TypesOfQuestionDto>> GetAllTypesAsync();
    Task<TypesOfQuestionDto> CreateTypeAsync(TypesOfQuestionCreateOrUpdateDto createContent);
    Task<TypesOfQuestionDto> UpdateTypeAsync(Guid id, TypesOfQuestionCreateOrUpdateDto updateContent);
    Task<bool> DeleteTypeAsync(Guid id);
}