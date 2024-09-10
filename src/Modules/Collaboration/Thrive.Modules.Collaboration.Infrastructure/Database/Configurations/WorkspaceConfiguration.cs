using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Thrive.Modules.Collaboration.Domain.Workspace.Entities;

namespace Thrive.Modules.Collaboration.Infrastructure.Database.Configurations;

internal sealed class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(w => w.Id);

        builder.OwnsOne(w => w.Name, wb =>
        {
            wb.Property(n => n.Value).HasColumnName("Name").IsRequired();
        });
        
        builder.OwnsOne(w => w.WorkspaceThreads, wtb =>
        {
            wtb.WithOwner().HasForeignKey("WorkspaceId");
            
            wtb.OwnsMany(wt => wt.Categories, cb =>
            {
                cb.HasKey(c => c.Id);
                cb.Property(c => c.Name).IsRequired();
                
                cb.WithOwner().HasForeignKey("WorkspaceId");
            });
            
            wtb.OwnsMany(wt => wt.Threads, tb =>
            {
                tb.WithOwner().HasForeignKey("WorkspaceId");
                tb.Property(t => t.Name).IsRequired();
                tb.HasKey(t => t.Id);
            });
        });
    }
}