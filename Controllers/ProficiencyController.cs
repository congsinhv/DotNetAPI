using DotnetAPIProject.Models.DTOs;
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
        // add
       
        [HttpPost("Proficiency")]
        public async Task<IActionResult> Create([FromBody] CreateProficiencyDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var created = await _proficiencyService.CreateProficiencyAsync(dto);
                return CreatedAtAction(nameof(GetById), new { idProficiency = created.Id }, created);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [HttpGet("{idProficiency}")]
        public async Task<IActionResult> GetById(Guid idProficiency)
        {
            var all = await _proficiencyService.GetAllAsync();
            var one = all.FirstOrDefault(x => x.Id == idProficiency);
            if (one == null) return NotFound();
            return Ok(one);
        }


    }
}
