using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IListeningQuestionService
{
    Task<ListeningQuestionNoAnswerDto> CreateListeningQuestionAsync(ListeningQuestionCreate createContent);
    Task<IEnumerable<ListeningQuestionNoAnswerDto>> GetAllListeningQuestionsAsync(string examId);
    
    // // Task<ListeningQuestionDto> UpdateListeningQuestionAsync(Guid id, ListeningQuestionCreateOrUpdateDto updateContent);
    // Task<bool> DeleteListeningQuestionAsync(Guid id);
}