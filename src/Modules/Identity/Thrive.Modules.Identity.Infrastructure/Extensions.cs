using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Domain.Repositories;
using Thrive.Modules.Identity.Infrastructure.Database;
using Thrive.Modules.Identity.Infrastructure.Database.Repositories;
using Thrive.Modules.Identity.Infrastructure.Options.Setups;
using Thrive.Modules.Identity.Infrastructure.Services;
using Thrive.Shared.Infrastructure.Database;

namespace Thrive.Modules.Identity.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureOptions<JwtOptionsSetup>();
        services.ConfigureOptions<JwtBearerOptionsSetup>();
        services.ConfigureOptions<EmailOptionsSetup>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddPostgres<IdentityContext>();
        services.AddScoped<ITokensProvider, TokensProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmTokenRepository, EmailConfirmTokenRepository>();
        services.AddScoped<IValueHasher, ValueHasher>();

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder builder)
    {
        builder.UseAuthentication();
        builder.UseAuthorization();

        return builder;
    }
}