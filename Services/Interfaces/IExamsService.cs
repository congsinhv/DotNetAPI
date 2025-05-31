using DotnetAPIProject.Models.DTOs;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IExamsService
    {
        Task<IEnumerable<ExamDto>> GetExamAsync(Guid? topicId);
        Task<ExamDto> CreateExamAsync(CreateExamDto examDto);

    }
}
