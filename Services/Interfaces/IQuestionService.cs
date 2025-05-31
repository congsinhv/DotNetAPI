
using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces;

public interface IQuestionService
{
    Task<QuestionNoAnswerDto> CreateQuestionAsync(QuestionCreate createContent);
    Task<IEnumerable<QuestionNoAnswerDto>> GetAllQuestionsByExamIdAsync(Guid examId);
}
