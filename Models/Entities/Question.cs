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
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string Content { get; set; } = string.Empty;

        [Required]
        public Guid ExamId { get; set; }

        [Required]
        public Guid TypeId { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public string TypeQuestion { get; set; } = string.Empty;
    }
} 