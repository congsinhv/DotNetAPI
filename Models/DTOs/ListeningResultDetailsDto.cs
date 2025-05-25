using DotnetAPIProject.Models.Entities;

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

    public class QuestionListening
    {
        public Guid Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Audio { get; set; } = string.Empty;
        public string? Script { get; set; }
        public string? Img { get; set; }
        public TypesOfQuestion Type { get; set; } = new TypesOfQuestion();
        public List<SelectedOption> Options { get; set; } = new List<SelectedOption>();
    }

    public class SelectedOption
    {
        public Guid Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool? IsSelected { get; set; }
        public bool? IsCorrect { get; set; }
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

    public class ListeningOptionDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public double FinishTime { get; set; }
        public Guid QuestionId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string AudioUrl { get; set; } = string.Empty;
        public string? Script { get; set; }
        public Guid TypeOfQuestionId { get; set; }
        public string TypeName { get; set; } = string.Empty;
        public string Symbol { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public string Description { get; set; } = string.Empty;
        public Guid AnswerId { get; set; }
        public int IsSelected { get; set; }
    }
}
