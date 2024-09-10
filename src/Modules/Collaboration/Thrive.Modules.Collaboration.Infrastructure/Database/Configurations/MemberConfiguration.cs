using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thrive.Modules.Collaboration.Domain.Member.Entities;
using Thrive.Modules.Collaboration.Domain.Workspace.Entities;

namespace Thrive.Modules.Collaboration.Infrastructure.Database.Configurations;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => new {m.Id, m.WorkspaceId});

        builder.HasOne<Workspace>().WithMany().HasForeignKey(m => m.WorkspaceId);
    }
}