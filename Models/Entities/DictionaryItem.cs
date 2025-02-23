using System.Text.Json.Serialization;

namespace DotnetAPIProject.Models.Entities;

public class DictionaryItem
{
    public Guid Id { get; set; }
    public required string Word { get; set; }
    public required string Definition { get; set; }

    public Guid WorkspaceId { get; set; }
}
