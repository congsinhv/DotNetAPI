namespace DotnetAPIProject.Models.DTOs
{
    public class ExamBaseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Topic { get; set; } = string.Empty;
        public required double Time { get; set; }
        public required string Skill { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProficiencyDto Proficiency { get; set; } = new ProficiencyDto();
    }

    public class ExamHaveAnswerDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Topic { get; set; } = string.Empty;
        public required double Time { get; set; }
        public required string Skill { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProficiencyDto Proficiency { get; set; } = new ProficiencyDto();
        public List<ListeningQuestionHaveAnswerDto> questions { get; set; } = new List<ListeningQuestionHaveAnswerDto>();
    }

    public class ExamNoAnswerDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string Topic { get; set; } = string.Empty;
        public required double Time { get; set; }
        public required string Skill { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ProficiencyDto Proficiency { get; set; } = new ProficiencyDto();
        public List<ListeningQuestionNoAnswerDto> questions { get; set; } = new List<ListeningQuestionNoAnswerDto>();
    }

    public class ExamCreate
    {
        public required string Name { get; set; }
        public string Topic { get; set; } = string.Empty;
        public required double Time { get; set; }
        public required string Skill { get; set; }
        public Guid ProficiencyId { get; set; }
    }
}