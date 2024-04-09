using Fleet.Modules.Identity.Domain.Entities;
using Fleet.Modules.Identity.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Fleet.Modules.Identity.Infrastructure.Database;

internal sealed class IdentityContext(DbContextOptions<IdentityContext> options) : DbContext(options)
{
    public DbSet<IdentityUser> Users => Set<IdentityUser>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<EmailConfirmationToken> EmailConfirmationTokens => Set<EmailConfirmationToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("identity");
        new IdentityUserConfiguration().Configure(builder.Entity<IdentityUser>());
        new RefreshTokenConfiguration().Configure(builder.Entity<RefreshToken>());
        new EmailConfirmationTokenConfiguration().Configure(builder.Entity<EmailConfirmationToken>());
    }
}