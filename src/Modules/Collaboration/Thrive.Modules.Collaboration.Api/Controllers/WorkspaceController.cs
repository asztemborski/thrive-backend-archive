using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thrive.Modules.Collaboration.Application.Commands.CreateWorkspaceCommand;
using Thrive.Modules.Collaboration.Application.Dtos;
using Thrive.Modules.Collaboration.Application.Queries.WorkspacesList;

namespace Thrive.Modules.Collaboration.Api.Controllers;

[Authorize]
public class WorkspaceController : BaseController
{
    private readonly ISender _sender;
    
    public WorkspaceController(ISender sender) =>_sender = sender;
    
    [HttpPost]
    public async Task CreateWorkspace(CreateWorkspaceCommand request, CancellationToken cancellationToken)
    {
        await _sender.Send(request, cancellationToken);
    }

    [HttpGet]
    public async Task<IEnumerable<WorkspacesListDto>> GetWorkspacesList(CancellationToken cancellationToken)
    {
        return await _sender.Send(new WorkspacesListQuery(), cancellationToken);
    }
}