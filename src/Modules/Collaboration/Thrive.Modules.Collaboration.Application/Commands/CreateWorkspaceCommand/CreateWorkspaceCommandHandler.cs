using MediatR;
using Thrive.Modules.Collaboration.Domain.Workspace.Entities;
using Thrive.Modules.Collaboration.Domain.Workspace.Repositories;
using Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Collaboration.Application.Commands.CreateWorkspaceCommand;

public sealed class CreateWorkspaceCommandHandler : IRequestHandler<CreateWorkspaceCommand>
{
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IIdentityModuleApi _identityModule;
    private readonly IPublisher _publisher;

    public CreateWorkspaceCommandHandler(IWorkspaceRepository workspaceRepository, IIdentityModuleApi identityModule,
        IPublisher publisher)
    {
        _workspaceRepository = workspaceRepository;
        _identityModule = identityModule;
        _publisher = publisher;
    }
    
    public async Task Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        var user = _identityModule.GetCurrentAuthenticatedUser();

        var owner = new Owner(user.Id, user.Username);
        var workspace = new Workspace(request.Name, request.Description, owner);
        await _workspaceRepository.AddAsync(workspace);
        
        foreach (var workspaceEvent in workspace.Events)
        {
            await _publisher.Publish(workspaceEvent, cancellationToken);
        }
    }
}