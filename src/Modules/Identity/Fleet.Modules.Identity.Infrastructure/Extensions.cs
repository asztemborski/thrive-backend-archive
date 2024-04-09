using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Domain.Repositories;
using Fleet.Modules.Identity.Infrastructure.Database;
using Fleet.Modules.Identity.Infrastructure.Database.Repositories;
using Fleet.Modules.Identity.Infrastructure.Options.Setups;
using Fleet.Modules.Identity.Infrastructure.Services;
using Fleet.Shared.Infrastructure.Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Fleet.Modules.Identity.Infrastructure;

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
        services.AddScoped<ITokensRequestStorage, TokensRequestStorage>();
        services.AddScoped<IConfirmationUriRequestStorage, ConfirmationUriRequestStorage>();
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