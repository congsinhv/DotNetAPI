using DotnetAPIProject.Data;
using DotnetAPIProject.Models.Entities;
using DotnetAPIProject.Models.DTOs;
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

    public async Task<IEnumerable<Workspace>> GetWorkspacesAsync()
    {
        return await _context.Workspaces.ToListAsync();
    }

    public async Task<Workspace> AddWorkspaceAsync(WorkspaceDto workspaceDto)
    {
        var workspace = new Workspace
        {
            Name = workspaceDto.Name,
            Description = workspaceDto.Description
        };

        _context.Workspaces.Add(workspace);
        await _context.SaveChangesAsync();
        return workspace;
    }

    public async Task<Workspace?> UpdateWorkspaceAsync(int id, WorkspaceDto workspaceDto)
    {
        var workspace = await _context.Workspaces.FindAsync(id);
        if (workspace == null) return null;

        workspace.Name = workspaceDto.Name;
        workspace.Description = workspaceDto.Description;

        await _context.SaveChangesAsync();
        return workspace;
    }

    public async Task<bool> DeleteWorkspaceAsync(int id)
    {
        var workspace = await _context.Workspaces.FindAsync(id);
        if (workspace == null) return false;

        _context.Workspaces.Remove(workspace);
        await _context.SaveChangesAsync();
        return true;
    }
} 