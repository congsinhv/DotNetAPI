using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    // Table representing a listening question in the exam system.
    [Table("ListeningQuestions")]
    public class ListeningQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string Question { get; set; }

        [Required]
        public required string AudioUrl { get; set; }

        [Required]
        public required string Script { get; set; }

        public string? ImageUrl { get; set; }
        

        [ForeignKey("TypesOfQuestions")]
        public Guid TypeOfQuestionId { get; set; }

        [ForeignKey("Exams")]
        public Guid ExamId { get; set; }
    }

}