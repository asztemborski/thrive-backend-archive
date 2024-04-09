using Fleet.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fleet.Modules.Identity.Infrastructure.Options.Setups;

internal sealed class JwtOptionsSetup : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration;

    public JwtOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(JwtOptions options)
    {
        _configuration.GetSection<JwtOptions>().Bind(options);
    }
}