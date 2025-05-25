using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IListeningResultService
{
    Task<ListeningResultDto> CreateListeningResultAsync(ListeningResultCreateDto createContent);
    Task<ResultResponseDto> GetDetailResultByResultIdAsync(Guid resultId);
    // Task<bool> DeleteListeningResultAsync(Guid id);
   
}