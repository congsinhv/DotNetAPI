namespace DotnetAPIProject.Models.DTOs
{
    public class ExamDto
    {
        public Guid IdExam { get; set; }
        public string NameExam { get; set; }
        public Guid TopicID { get; set; }
        public string TopicName { get; set; }
        public string Skill { get; set; }

    }

    public class ExamHaveAnswerResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Time { get; set; }
        public required string Skill { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TopicDto Topic { get; set; }
        public List<QuestionNoAnswerDto> questions { get; set; } = new List<QuestionNoAnswerDto>();
    }
}
