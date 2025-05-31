namespace DotnetAPIProject.Models.DTOs
{
    public class TopicDto
    {
        public Guid IdTopic { get; set; }              // sửa từ int -> Guid
        public string Name { get; set; }
        public Guid IdProficiency { get; set; }        // sửa từ int -> Guid
        public string NameProficiency { get; set; }
    }

}
