using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPIProject.Controllers
{
    public class ProficiencyController : ControllerBase
    {
        private readonly IProficiencyService _proficiencyService;

        public ProficiencyController(IProficiencyService proficiencyService)
        {
            _proficiencyService = proficiencyService;
        }

        [HttpGet("Proficiency")]
        public async Task<ActionResult<IEnumerable<Proficiency>>> GetLevel()
        {
            var proficiency = await _proficiencyService.GetProficiencyAsync();
            return Ok(proficiency);
        }
        //[HttpPost("Proficiency")]
        //public async Task<IActionResult> CreateProficiency([FromBody] Proficiency proficiency)
        //{
        //    try
        //    {
        //        var createdProficiency = await _proficiencyService.CreateProficiencyAsync(proficiency);
        //        return CreatedAtAction(nameof(createdProficiency), new { id = proficiency.Id }, createdProficiency);

        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


    }
}
