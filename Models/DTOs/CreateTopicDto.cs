namespace DotnetAPIProject.Models.DTOs
{
    public class CreateTopicDto
    {
        public string TopicName { get; set; }
        public Guid ProficiencyId { get; set; }
    }
}
