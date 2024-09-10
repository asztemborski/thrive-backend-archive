using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Collaboration.Domain.Workspace.Entities;
using Thrive.Modules.Collaboration.Domain.Workspace.Repositories;

namespace Thrive.Modules.Collaboration.Infrastructure.Database.Repositories;

internal sealed class WorkspaceRepository : IWorkspaceRepository
{
    private readonly CollaborationContext _context;

    public WorkspaceRepository(CollaborationContext context) => _context = context;
    
    public Task<Workspace?> GetById(Guid guid)
    {
        return _context.Workspaces.FirstOrDefaultAsync(w => w.Id == guid);
    }

    public async Task AddAsync(Workspace workspace)
    {
        await _context.Workspaces.AddAsync(workspace);
        await _context.SaveChangesAsync();
    }

    public Task<List<Workspace>> GetByMemberId(Guid memberId)
    {
      return _context.Workspaces.Join(_context.Members, w => w.Id, m => m.WorkspaceId, 
              (workspace, member) => new { workspace, member })
            .Where(wm => wm.member.Id == memberId)
            .Select(wm => wm.workspace)
            .ToListAsync();
    }
}