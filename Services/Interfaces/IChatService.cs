using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChatAsync(ChatDto chat);
        Task<List<Chat>> GetChatsByUserIdAsync(int userId);
        Task<List<DetailChat>> GetDetailChatByIdAsync(Guid chatId);
        Task<bool> DeleteChatAsync(Guid chatId);
        Task<DetailChat> PostRequestChat(DetailChatDto request);
    }
}