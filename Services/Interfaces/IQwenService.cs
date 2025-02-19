using DotnetAPIProject.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IQwenService
    {
        Task<List<ChatHistory>> GetChatHistoryAsync(int userId);
        Task<ChatHistory> CreateChatHistoryAsync(ChatHistory chatHistory);
        Task<bool> UpdateChatHistoryAsync(int chatHistoryId, int userId, ChatHistory chatHistory);
        Task<bool> DeleteChatHistoryAsync(int chatHistoryId);
    }
}
