namespace DotnetAPIProject.Models.DTOs
{
    public class ProficiencyDto
    {
        public Guid Id { get; set; }
        public string Band { get; set; }  = string.Empty;
        public string Name { get; set; } = string.Empty;
         public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class ProficiencyCreateOrUpdateDto
    {
        public string? Band { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}