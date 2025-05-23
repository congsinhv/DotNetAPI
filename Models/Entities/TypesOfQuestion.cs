namespace DotnetAPIProject.Models.Entities;

public class TypesOfQuestionBase
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
}

public class TypesOfQuestionCreateOrUpdate : TypesOfQuestionBase
{
    public required string Name { get; set; }
    public string Description { get; set; }
}

public class TypesOfQuestionResponse : TypesOfQuestionBase
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

