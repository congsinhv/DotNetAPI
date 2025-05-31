
using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IListeningQuestionService
{
    Task<QuestionNoAnswerDto> CreateQuestionAsync(QuestionCreate createContent);
    Task<IEnumerable<QuestionNoAnswerDto>> GetAllQuestionsByExamIdAsync(Guid examId);
}
