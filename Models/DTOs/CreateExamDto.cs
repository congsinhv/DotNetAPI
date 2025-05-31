namespace DotnetAPIProject.Models.DTOs
{
    public class CreateExamDto
    {
        public string NameExam { get; set; }
        public Guid TopicID { get; set; }
        public int Time { get; set; } // thời gian thi
        public string Skill { get; set; }
    }
}
