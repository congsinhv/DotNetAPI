namespace DotnetAPIProject.Models.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string NumberPhone { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
