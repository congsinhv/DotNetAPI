using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetAPIProject.Models.Entities
{
    [Table("DetailResult")]
    public class DetailResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid ResultId { get; set; }

        [Required]
        public Guid AnswerId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        [ForeignKey("ResultId")]
        public virtual UserExam ResultUserExam { get; set; }

        [ForeignKey("AnswerId")]
        public virtual Answer Answer { get; set; }

        [ForeignKey("QuestionId")]
        public virtual Question Question { get; set; }
    }
} 