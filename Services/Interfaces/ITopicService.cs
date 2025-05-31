using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicResponseDto>> GetTopicsAsync(Guid ProficiencyId);
        Task<TopicDto> GetTopicByIdAsync(Guid topicId);
    }
}
