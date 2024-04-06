using Fleet.Modules.Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Fleet.Modules.Identity.Infrastructure.Database.Configurations;

internal sealed class IdentityUserConfiguration : IEntityTypeConfiguration<IdentityUser>
{
    public void Configure(EntityTypeBuilder<IdentityUser> builder)
    {
        builder.OwnsOne(user => user.Email, userEmailBuilder =>
        {
            userEmailBuilder.OwnsOne(userEmail => userEmail.Address,
                addressBuilder =>
                {
                    addressBuilder.Property(email => email.Value).HasColumnName("EmailAddress").IsRequired();
                });

            userEmailBuilder.Property(userEmail => userEmail.IsConfirmed).HasColumnName("EmailConfirmed")
                .IsRequired();
        });

        builder.OwnsOne(user => user.Username, navigationBuilder =>
        {
            navigationBuilder.Property(username => username.Value)
                .HasColumnName("Username").IsRequired();
        });
    }
}