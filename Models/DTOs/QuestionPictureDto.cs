namespace DotnetAPIProject.Models.DTOs
{
    public class QuestionPictureDto
    {
        public Guid QuestionID { get; set; }
        public string QuestionType { get; set; } 
        public string? TextQuestion { get; set; } 
        public string? CorrectAnswer { get; set; } 
        public string? CorrectImg { get; set; } 
        public List<ImageOptionDto>? ImageOptions { get; set; } 
    }

    public class ImageOptionDto
    {
        public string? QuestionImagesText { get; set; }
        public string? ImageCode { get; set; } 
        public int IsCorrect { get; set; } 
    }
    public class CreateUserExamHistoryDto
    {
        public Guid UserId { get; set; }
        public Guid ExamId { get; set; }
    }
    // posst text
    public class PostQuestionTextRequest
    {
        public Guid ExamId { get; set; }
        public string TextQuestion { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
        public string CorrectImg { get; set; } = string.Empty;
    }
    // post img
    public class PostQuestionImageRequest
    {
        public Guid ExamId { get; set; }
        public string QuestionImagesText { get; set; } = string.Empty;
        public string CorrectImg { get; set; } = string.Empty;
        public string WrongImg1 { get; set; } = string.Empty;
        public string WrongImg2 { get; set; } = string.Empty;
    }

}
