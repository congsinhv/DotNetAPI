namespace DotnetAPIProject.Models.DTOs
{
    public class ListeningQuestionHaveAnswerDto
    {
        public Guid Id { get; set; }
        public required string Question { get; set; } = string.Empty;
        public required string AudioUrl { get; set; } = string.Empty;
        public required string Script { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool? isMarked { get; set; }
        public string? DescriptionResult { get; set; }
        public required TypesOfQuestionDto Type { get; set; }
        public required List<ListeningAnswer> Options { get; set; } = new List<ListeningAnswer>();
    }

    public class ListeningQuestionNoAnswerDto
    {
        public Guid Id { get; set; }
        public required string Question { get; set; } = string.Empty;
        public required string AudioUrl { get; set; } = string.Empty;
        public required string Script { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public bool? isMarked { get; set; }
        public required TypesOfQuestionDto Type { get; set; }
        public required List<ListeningAnswerResponseDto> options { get; set; } = new List<ListeningAnswerResponseDto>();
    }

    public class ListeningQuestionCreate
    {
        public required string Question { get; set; } = string.Empty;
        public required string AudioUrl { get; set; } = string.Empty;
        public required string Script { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid TypeOfQuestionId { get; set; }
        public Guid ExamId { get; set; }
        public List<ListeningAnswerCreate> options { get; set; } = new List<ListeningAnswerCreate>();
    }

    public class ListeningQuestionUpdate
    {
        public string? Question { get; set; } = string.Empty;
        public string? AudioUrl { get; set; } = string.Empty;
        public string? Script { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public Guid TypeOfQuestionId { get; set; }
    }
}