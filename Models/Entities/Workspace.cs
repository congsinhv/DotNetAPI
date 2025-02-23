namespace DotnetAPIProject.Models.Entities;

public class Workspace
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}


public class WorkspaceResponse: Workspace
{
    public int WordCount { get; set; }
}