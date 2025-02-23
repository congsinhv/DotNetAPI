using DotnetAPIProject.Data;
using DotnetAPIProject.Models.DTOs;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPIProject.Services.Implementations;

public class WorkspaceService : IWorkspaceService
{
    private readonly ApplicationDbContext _context;

    public WorkspaceService(ApplicationDbContext context)
    {
        _context = context;
    }

    

    public async Task<IEnumerable<WorkspaceResponse>> GetWorkspacesAsync()
    {
        var workspaces = await _context.Workspaces
            .Select(w => new WorkspaceResponse
            {
                Id = w.Id,
                Name = w.Name,
                Description = w.Description,
                WordCount = _context.DictionaryItems.Count(di => di.WorkspaceId == w.Id)
            })
            .ToListAsync();

        return workspaces;
    }


    public async Task<Workspace> AddWorkspaceAsync(WorkspaceDto workspaceDto)
    {
        var workspace = new Workspace
        {
            Name = workspaceDto.Name,
            Description = workspaceDto.Description,
        };

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();
        return workspace;
    }

    public async Task<Workspace?> UpdateWorkspaceAsync(int id, WorkspaceDto workspaceDto)
    {
        var workspace = await _context.Workspaces.FindAsync(id);
        if (workspace == null)
            return null;

        workspace.Name = workspaceDto.Name;
        workspace.Description = workspaceDto.Description;

        await _context.SaveChangesAsync();
        return workspace;
    }

    public async Task<Workspace?> DeleteWorkspaceAsync(int id)
    {
        var workspace = await _context.Workspaces.FindAsync(id);
        if (workspace == null)
            return null;
         _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
        return workspace;
    }
}
