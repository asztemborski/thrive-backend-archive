using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Collaboration.Domain.Member.Entities;
using Thrive.Modules.Collaboration.Domain.Workspace.Entities;
using Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;
using Thrive.Modules.Collaboration.Infrastructure.Database.Configurations;

namespace Thrive.Modules.Collaboration.Infrastructure.Database;

public sealed class CollaborationContext(DbContextOptions<CollaborationContext> options) : DbContext(options)
{
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Workspace> Workspaces => Set<Workspace>();

    private DbSet<Role> Roles => Set<Role>();
    private DbSet<Permission> Permissions => Set<Permission>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("collaboration");
        new MemberConfiguration().Configure(modelBuilder.Entity<Member>());
        new WorkspaceConfiguration().Configure(modelBuilder.Entity<Workspace>());
        new RoleConfiguration().Configure(modelBuilder.Entity<Role>());
        new PermissionConfiguration().Configure(modelBuilder.Entity<Permission>());
    }
}