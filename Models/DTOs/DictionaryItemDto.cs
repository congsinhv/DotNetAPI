using System.Text.Json.Serialization;

namespace DotnetAPIProject.Models.DTOs;

public class DictionaryItemDto
{
    public  string Word { get; set; } = string.Empty;
    public string Definition { get; set; } = string.Empty;
    public int WorkspaceId { get; set; }
    public string Phonetic { get; set; }
    public List<PhoneticDTO> Phonetics { get; set; }
    public List<MeaningDTO> Meanings { get; set; }

    public class PhoneticDTO
    {
        public string Text { get; set; }
        public string Audio { get; set; }
    }

    public class MeaningDTO
    {
        public string PartOfSpeech { get; set; }
        public List<DefinitionDTO> Definitions { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; }
    }

    public class DefinitionDTO
    {
        public string Definitional { get; set; }
        public string Example { get; set; }
        public List<string> Synonyms { get; set; }
        public List<string> Antonyms { get; set; } 
    }
}

public class WordDefinitionDto
{
    public required string Word { get; set; }
}
