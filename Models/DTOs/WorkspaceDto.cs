using System.Collections.Generic;

namespace DotnetAPIProject.Models.DTOs;

public class WorkspaceDto
{
    public required string Name { get; set; }

    public int WordCount { get; set; }
    public required string Description { get; set; }
   // public ICollection<DictionaryItemDto> Dictionaries { get; set; } = new List<DictionaryItemDto>();
}
