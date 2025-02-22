﻿namespace DotnetAPIProject.Models.DTOs
{
    public class ChatHistoryDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
