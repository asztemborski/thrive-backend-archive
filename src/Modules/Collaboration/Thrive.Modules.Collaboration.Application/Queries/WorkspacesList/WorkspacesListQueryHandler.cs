using MediatR;
using Thrive.Modules.Collaboration.Application.Dtos;
using Thrive.Modules.Collaboration.Domain.Workspace.Repositories;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Collaboration.Application.Queries.WorkspacesList;

internal sealed class WorkspacesListQueryHandler : IRequestHandler<WorkspacesListQuery, IEnumerable<WorkspacesListDto>>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IIdentityModuleApi _identityModule;

    public WorkspacesListQueryHandler(IWorkspaceRepository workspaceRepository, IIdentityModuleApi identityModule)
    {
        _workspaceRepository = workspaceRepository;
        _identityModule = identityModule;
    }

    public async Task<IEnumerable<WorkspacesListDto>> Handle(WorkspacesListQuery request, CancellationToken cancellationToken)
    {
        var user = _identityModule.GetCurrentAuthenticatedUser();
        var workspaces = await _workspaceRepository.GetByMemberId(user.Id);

        return workspaces.Select(w => new WorkspacesListDto
        {
            Id = w.Id,
            Name = w.Name,
            Description = w.Description
        });
    }
}