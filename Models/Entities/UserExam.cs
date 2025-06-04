using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    [Table("UserExam")]
    public class UserExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ExamId { get; set; }

        [Required]
        public DateTime FinishedTime { get; set; }

        [Required]
        public double OverallScore { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        [ForeignKey("UserId")]
        public virtual Account Account { get; set; }

        [ForeignKey("ExamId")]
        public virtual Exam Exam { get; set; }
    }
} 