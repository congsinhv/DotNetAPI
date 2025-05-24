using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    [Table("ListeningResults")]
    public class ListeningResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required Guid UserId { get; set; }

        [Required]
        public required Guid ExamId { get; set; }

        [Required]
        public required float FinishTime { get; set; }

        public string? OverallScore { get; set; }
        

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}