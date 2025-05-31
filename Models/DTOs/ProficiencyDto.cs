using System.ComponentModel.DataAnnotations;
namespace DotnetAPIProject.Models.DTOs
{
    public class ProficiencyDto
    {
        [Required, MaxLength(20)]
        public string Band { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }
    }

    public class ProficiencyResponseDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Band { get; set; }
        public required string Description { get; set; }
    }
}