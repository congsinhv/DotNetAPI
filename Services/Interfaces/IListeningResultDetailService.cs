using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IListeningResultDetailService
{
    Task<ListeningResultDetailsDto> CreateListeningResultDetailAsync(Guid listeningResultId, ListeningResultDetailCreateDto createContent);
    Task<List<ListeningResultDetailsDto>> GetListeningResultDetailsByResultIdAsync(Guid resultId);

    Task<List<ListeningResultDetailsDto>> GetCorrectResultDetailByResultIdAsync(Guid resultId);

    Task<ListeningResultDetailsDto> UpdateListeningResultDetailAsync(Guid id, ListeningResultDetailUpdateDto updateContent);
}