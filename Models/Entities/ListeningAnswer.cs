using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{ 

    // This table represents an answer to a listening question in the exam system.
    [Table("ListeningAnswers")]
    public class ListeningAnswer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string symbol { get; set; }


        [Required]
        public required string description { get; set; }


        [Required]
        public required bool IsCorrect { get; set; }


        [ForeignKey("ListeningQuestions")]
        public Guid ListeningQuestionId { get; set; }

    }
}