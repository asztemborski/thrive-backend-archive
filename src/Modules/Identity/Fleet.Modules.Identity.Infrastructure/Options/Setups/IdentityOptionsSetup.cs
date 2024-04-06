using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fleet.Modules.Identity.Infrastructure.Options.Setups;

public sealed class IdentityOptionsSetup : IConfigureOptions<IdentityOptions>
{
    private readonly IConfiguration _configuration;
    private const string SectionName = "identity";

    public IdentityOptionsSetup(IConfiguration configuration) => _configuration = configuration;
    
    public void Configure(IdentityOptions options)
    { 
        _configuration.GetSection($"{SectionName}:{nameof(IdentityOptions)}").Bind(options);
    }
}