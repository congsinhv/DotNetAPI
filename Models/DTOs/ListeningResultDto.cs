namespace DotnetAPIProject.Models.DTOs
{
    public class ListeningResultDto
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid ExamId { get; set; }
        public required float FinishTime { get; set; }
        public string? OverallScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class ListeningResultHaveAnswerDto
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required float FinishTime { get; set; }
        public string? OverallScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public  List<ListeningResultDetailResponseDto> results { get; set; } = new List<ListeningResultDetailResponseDto>();
    }

    public class ListeningResultCreateDto
    {
        public required Guid UserId { get; set; }
        public required Guid ExamId { get; set; }
        public required float FinishTime { get; set; }

        public List<ListeningResultDetailCreateDto> UserAnswers { get; set; } = new List<ListeningResultDetailCreateDto>();
    }
}