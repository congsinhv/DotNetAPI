using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IExamService
    {
        Task<IEnumerable<ExamBaseDto>> GetExamsByNameAsync(string name);
        Task<ExamBaseDto> CreateExamAsync(ExamCreate exam);
        Task<bool> DeleteExamAsync(Guid examId);

        Task<ExamNoAnswerDto> GetExamByIdAsync(Guid examId);
        
        // Task<DetailExam> PostRequestExam(DetailExamDto request);
    }
}