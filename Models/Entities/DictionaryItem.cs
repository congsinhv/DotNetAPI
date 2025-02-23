using System.Text.Json.Serialization;

namespace DotnetAPIProject.Models.Entities;

public class DictionaryItem
{
    public int Id { get; set; }
    public required string Word { get; set; }
    public required string Definition { get; set; }

    public int WorkspaceId { get; set; }
}
