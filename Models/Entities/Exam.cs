using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    // This table represents an exam in the exam system.
    [Table("Exams")]
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public string? Topic { get; set; }

        [Required]
        public required double Time { get; set; }

        [Required]
        public required string Skill { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("Proficiencies")]
        public Guid ProficiencyId { get; set; }
    }
}