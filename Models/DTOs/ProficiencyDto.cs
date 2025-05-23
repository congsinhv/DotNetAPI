namespace DotnetAPIProject.Models.DTOs
{
    public class ProficiencyDto
    {
        public Guid Id { get; set; }
        public string Band { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ProficiencyDto() { }
        public ProficiencyDto(Guid id, string band, string name, string description, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Band = band;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }

    public class ProficiencyCreateOrUpdateDto
    {
        public string? Band { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}