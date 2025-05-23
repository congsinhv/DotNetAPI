using System.Collections.Generic;
using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IListeningAnswerService
    {
        Task<ListeningAnswer> CreateListeningAnswerAsync(string questionId,ListeningAnswerCreate listeningAnswerCreate);

        //Just get all answers by questionId (not include isCorrect)
        Task<List<ListeningAnswerResponseDto>> GetListeningAnswersByQuestionIdAsync(Guid questionId);
        Task<bool> DeleteListeningAnswerAsync(Guid id);
        Task<ListeningAnswer> UpdateListeningAnswerAsync(Guid id, ListeningAnswerUpdate listeningAnswerUpdate);
    }
}
