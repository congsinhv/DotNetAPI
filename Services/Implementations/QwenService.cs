using DotnetAPIProject.Data;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetAPIProject.Services.Implementations
{
    public class QwenService : IQwenService
    {
        private readonly ApplicationDbContext _context;

        public QwenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ChatHistory>> GetChatHistoryAsync(int userId)
        {
            try
            {
                return await _context.ChatHistories
                    .AsNoTracking()
                    .Where(ch => ch.UserId == userId)
                    .OrderByDescending(ch => ch.CreatedAt)
                    .ToListAsync();
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database error in GetChatHistoryAsync: {dbEx.Message}");
                throw new Exception("Lỗi truy vấn cơ sở dữ liệu.", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error in GetChatHistoryAsync: {ex.Message}");
                throw;
            }
        }
        public async Task<ChatHistory> CreateChatHistoryAsync(ChatHistory chatHistory)
        {
            _context.ChatHistories.Add(chatHistory);
            await _context.SaveChangesAsync();
            return chatHistory;
        }

        public async Task<bool> UpdateChatHistoryAsync(int chatHistoryId, int userId, ChatHistory chatHistory)
        {
            chatHistory.Id = chatHistoryId;
            chatHistory.UserId = userId;
            _context.ChatHistories.Update(chatHistory);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteChatHistoryAsync(int chatHistoryId)
        {
            var chatHistory = await _context.ChatHistories.FindAsync(chatHistoryId);
            if (chatHistory == null) return false;

            _context.ChatHistories.Remove(chatHistory);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
