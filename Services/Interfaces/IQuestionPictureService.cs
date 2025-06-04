using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IQuestionPictureService
    {
        Task<IEnumerable<QuestionPictureDto>> GetQuestionsAsync(Guid questionSetId);
        Task<IEnumerable<UserExam>> GetResultUserExamAsync();
        Task<UserExam> CreateUserExamHistoryAsync(CreateUserExamHistoryDto dto);
        // post
        Task<Guid> CreateQuestionTextAsync(PostQuestionTextRequest request);
        Task<Guid> CreateQuestionImageAsync(PostQuestionImageRequest request); 
    }
}
