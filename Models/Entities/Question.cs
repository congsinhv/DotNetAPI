using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    [Table("Questions")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public required string Content { get; set; } = string.Empty;


        [ForeignKey("ExamId")]
        public required Guid ExamId { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        public string TypeQuestion { get; set; } = string.Empty;
    }
} 