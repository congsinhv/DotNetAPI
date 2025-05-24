namespace DotnetAPIProject.Models.DTOs
{
    public class ListeningResultDetailsDto
    {
        public Guid Id { get; set; }
        public Guid ListeningResultId { get; set; }
        public Guid ListeningQuestionId { get; set; }
        public bool IsMarked { get; set; }
        public string? Status { get; set; } // correct, incorrect, not answered
        public Guid? AnswerId { get; set; }
    }

    public class ListeningResultDetailResponseDto
    {
        public Guid Id { get; set; }
        public ListeningQuestionNoAnswerDto ListeningQuestion { get; set; }
        public bool IsMarked { get; set; }
        public string? Status { get; set; } // correct, incorrect, not answered

    }
    public class ListeningResultDetailCreateDto
    {
        public Guid ListeningQuestionId { get; set; }
        public string? AnswerId { get; set; }
        public bool IsMarked { get; set; }
    }

    public class ListeningResultDetailUpdateDto
    {
        public string? Status { get; set; } // correct, incorrect, not answered
    }
}