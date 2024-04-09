using Fleet.Modules.Identity.Application.Options;
using Fleet.Shared.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Fleet.Modules.Identity.Infrastructure.Options.Setups;

internal sealed class EmailOptionsSetup : IConfigureOptions<EmailOptions>
{
    private readonly IConfiguration _configuration;

    public EmailOptionsSetup(IConfiguration configuration) => _configuration = configuration;

    public void Configure(EmailOptions options)
    {
        _configuration.GetSection<EmailOptions>().Bind(options);
    }
}