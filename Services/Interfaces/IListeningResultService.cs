using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IListeningResultService
{
    Task<ListeningResultDto> CreateListeningResultAsync(ListeningResultCreateDto createContent);
    Task<ListeningResultHaveAnswerDto> GetDetailResultByResultIdAsync(Guid resultId);
    // Task<bool> DeleteListeningResultAsync(Guid id);
   
}