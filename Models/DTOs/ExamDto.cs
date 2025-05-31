namespace DotnetAPIProject.Models.DTOs
{
    public class ExamDto
    {
        public Guid IdExam { get; set; }
        public string NameExam { get; set; }
        public Guid TopicID { get; set; }
        public string TopicName { get; set; }
    
    }
}
