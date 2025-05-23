using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    [Table("ListeningResultDetails")]
    public class ListeningResultDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }


        [ForeignKey("ListeningResult")]
        public Guid ListeningResultId { get; set; }

        [ForeignKey("ListeningQuestion")]
        public Guid ListeningQuestionId { get; set; }

        public bool? isMarked { get; set; }

        public string? Status { get; set; } // correct, incorrect, not answered


        [ForeignKey("ListeningAnswer")]
        public string? AnswerId { get; set; }
    }
}

