using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotnetAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QwenController : ControllerBase
    {
        private readonly IQwenService _qwenService;

        public QwenController(IQwenService qwenService)
        {
            _qwenService = qwenService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetChatHistory(int userId)
        {
            var chatHistory = await _qwenService.GetChatHistoryAsync(userId);

            if (chatHistory == null || chatHistory.Count == 0)
            {
                return NotFound("Không có lịch sử chat cho userId này.");
            }

            return Ok(chatHistory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChatHistory([FromBody] ChatHistory chatHistory)
        {
            if (chatHistory == null || chatHistory.UserId <= 0 || string.IsNullOrEmpty(chatHistory.Title))
            {
                return BadRequest("Dữ liệu không hợp lệ. Vui lòng cung cấp userId và title.");
            }

            var createdChatHistory = await _qwenService.CreateChatHistoryAsync(chatHistory);
            return CreatedAtAction(nameof(GetChatHistory), new { userId = createdChatHistory.UserId }, createdChatHistory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateChatHistory(int chatHistoryId,int userId, [FromBody] ChatHistory chatHistory)
        {
            if (chatHistory == null)
                return BadRequest("Dữ liệu không hợp lệ.");

            var updated = await _qwenService.UpdateChatHistoryAsync(chatHistoryId, userId, chatHistory);

            if (!updated) 
                return NotFound("Không tìm thấy lịch sử chat cần cập nhật.");

            return NoContent();
        }

        [HttpDelete("{chatHistoryId}")]
        public async Task<IActionResult> DeleteChatHistory(int chatHistoryId)
        {
            var deleted = await _qwenService.DeleteChatHistoryAsync(chatHistoryId);
            if (!deleted)
            {
                return NotFound("Không tìm thấy lịch sử chat cần xóa.");
            }

            return NoContent();
        }
    }
}
