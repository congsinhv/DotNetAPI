using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IExamsService
    {
        // Get all exams by topic id
        Task<IEnumerable<ExamDto>> GetExamAsync(Guid? topicId);

        // Get detail exam by id
        Task<ExamHaveAnswerResponseDto> GetDetailExamByIdAsync(Guid examId);
    }
}
