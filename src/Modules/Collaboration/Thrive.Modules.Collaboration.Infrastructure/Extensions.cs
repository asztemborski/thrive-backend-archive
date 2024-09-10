using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Collaboration.Domain.Member.Repositories;
using Thrive.Modules.Collaboration.Domain.Workspace.Repositories;
using Thrive.Modules.Collaboration.Infrastructure.AuthorizationPolicies.Handlers;
using Thrive.Modules.Collaboration.Infrastructure.Database;
using Thrive.Modules.Collaboration.Infrastructure.Database.Repositories;
using Thrive.Shared.Infrastructure.Database;

namespace Thrive.Modules.Collaboration.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddPostgres<CollaborationContext>();

        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IAuthorizationHandler, WorkspaceMemberHandler>();
        
        return services;
    }
}