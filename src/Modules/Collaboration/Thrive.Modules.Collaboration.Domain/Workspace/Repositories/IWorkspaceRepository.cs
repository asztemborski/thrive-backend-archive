namespace Thrive.Modules.Collaboration.Domain.Workspace.Repositories;

public interface IWorkspaceRepository
{
    Task<Entities.Workspace?> GetById(Guid guid);
    Task AddAsync(Entities.Workspace workspace);
    Task<List<Entities.Workspace>> GetByMemberId(Guid memberId);
}