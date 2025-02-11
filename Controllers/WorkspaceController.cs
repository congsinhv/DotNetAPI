// using Microsoft.AspNetCore.Mvc;
// using DotnetAPIProject.Services.Interfaces;
// using DotnetAPIProject.Models.DTOs;
// using DotnetAPIProject.Models.Entities;

// namespace DotnetAPIProject.Controllers;

// [ApiController]
// [Route("api/v1/[controller]")]
// public class WorkspaceController : ControllerBase
// {
//     private readonly IWorkspaceService _workspaceService;

//     public WorkspaceController(IWorkspaceService workspaceService)
//     {
//         _workspaceService = workspaceService;
//     }

//     [HttpGet]
//     public async Task<ActionResult<IEnumerable<Workspace>>> GetWorkspaces()
//     {
//         var workspaces = await _workspaceService.GetWorkspacesAsync();
//         return Ok(workspaces);
//     }

//     [HttpPost]
//     public async Task<ActionResult<Workspace>> CreateWorkspace(WorkspaceDto workspaceDto)
//     {
//         var workspace = await _workspaceService.AddWorkspaceAsync(workspaceDto);
//         return CreatedAtAction(nameof(GetWorkspaces), new { id = workspace.Id }, workspace);
//     }

//     [HttpPut("{id}")]
//     public async Task<ActionResult<Workspace>> UpdateWorkspace(int id, WorkspaceDto workspaceDto)
//     {
//         var workspace = await _workspaceService.UpdateWorkspaceAsync(id, workspaceDto);
//         if (workspace == null)
//         {
//             return NotFound();
//         }
//         return Ok(workspace);
//     }

//     [HttpDelete("{id}")]
//     public async Task<IActionResult> DeleteWorkspace(int id)
//     {
//         var result = await _workspaceService.DeleteWorkspaceAsync(id);
//         if (!result)
//         {
//             return NotFound();
//         }
//         return NoContent();
//     }
// } 