using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Collaboration.Infrastructure.AuthorizationPolicies.Requirements;
using Thrive.Modules.Collaboration.Infrastructure.Database;
using Thrive.Modules.Identity.Shared;

namespace Thrive.Modules.Collaboration.Infrastructure.AuthorizationPolicies.Handlers;

internal sealed class WorkspaceMemberHandler : AuthorizationHandler<WorkspaceMemberRequirement>
{
    private readonly CollaborationContext _context;
    private readonly IIdentityModuleApi _identityModule;

    public WorkspaceMemberHandler(CollaborationContext context, IIdentityModuleApi identityModule)
    {
        _context = context;
        _identityModule = identityModule;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        WorkspaceMemberRequirement requirement)
    {
        var currentUser = _identityModule.GetCurrentAuthenticatedUser();
        
        if (context.Resource is HttpContext httpContext)
        {
            var workspaceIdParam = httpContext.GetRouteValue("workspaceId");

            if (workspaceIdParam is null)
            {
                return;
            }
            
            var workspaceId = Guid.Parse(workspaceIdParam.ToString() ?? string.Empty);

            var exists = await _context.Members
                .AnyAsync(m => m.Id == currentUser.Id && m.WorkspaceId == workspaceId);

            if (!exists)
            {
                return;
            }

            context.Succeed(requirement);
        }
    }
}