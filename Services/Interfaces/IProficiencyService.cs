using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces
{
    public interface IProficiencyService
    {
        Task<IEnumerable<Proficiency>> GetProficiencyAsync();
        //Task<Proficiency> CreateProficiencyAsync(Proficiency proficiency); // Add this line

    }
}
