namespace DotnetAPIProject.Models.DTOs
{
    public class ListeningAnswer
    {
        public Guid Id { get; set; }
        public required string symbol { get; set; }
        public required string description { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class ListeningAnswerResponseDto
    {
        public Guid Id { get; set; }
        public required string symbol { get; set; }
        public required string description { get; set; }
    }

    public class ListeningAnswerCreate
    {
        public string? symbol { get; set; }
        public string? description { get; set; }
        public bool? IsCorrect { get; set; }
    }

    public class ListeningAnswerUpdate
    {
        public string? symbol { get; set; }
        public string? description { get; set; }
        public bool? IsCorrect { get; set; }
    }
}