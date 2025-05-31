namespace DotnetAPIProject.Models.DTOs
{
    public class TopicDto
    {
        public Guid IdTopic { get; set; }          
        public string Name { get; set; }
        public Guid IdProficiency { get; set; }      
        public string NameProficiency { get; set; }
    }

}
