using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;

namespace DotnetAPIProject.Services.Interfaces;

public interface IWorkspaceService
{
    Task<IEnumerable<WorkspaceResponse>> GetWorkspacesAsync();
    Task<Workspace> AddWorkspaceAsync(WorkspaceDto workspace);
    Task<Workspace?> UpdateWorkspaceAsync(Guid id, WorkspaceDto workspace);
    Task<Workspace> DeleteWorkspaceAsync(Guid id);
}
