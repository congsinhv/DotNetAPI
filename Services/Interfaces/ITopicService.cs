using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDto>> GetTopicsAsync(Guid? ProficiencyId);
        Task<TopicDto> CreateTopicAsync(CreateTopicDto dto);
        //Task<List<TopicDto>> GetTopicsAsync(Guid? proficiencyId);

    }
}
