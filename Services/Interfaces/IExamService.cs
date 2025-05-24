using System.Collections.Generic;
using System.Threading.Tasks;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IExamService
    {
        Task<ExamBaseDto> CheckExistExam(string name, string proficiencyId);
        Task<ExamNoAnswerDto> GetExamByIdAsync(Guid examId);
        Task<IEnumerable<ExamNoAnswerDto>> GetExamsByProficiencyIdAsync(Guid proficiencyId);
        Task<ExamBaseDto> CreateExamAsync(ExamCreate exam);
        Task<bool> DeleteExamAsync(Guid examId);
        
        // Task<DetailExam> PostRequestExam(DetailExamDto request);
    }
}