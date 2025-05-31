using System.ComponentModel.DataAnnotations;

public class ProficiencyDto
{
    public Guid Id { get; set; }

    [Required, MaxLength(20)]
    public string Band { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(255)]
    public string Description { get; set; }
}