using MediatR;
using Microsoft.Extensions.Options;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Options;
using Thrive.Modules.Identity.Domain.Repositories;

namespace Thrive.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;

internal sealed class EmailConfirmationUriCommandHandler : IRequestHandler<EmailConfirmationUriCommand, string>
{
    private readonly ITokensProvider _tokensProvider;
    private readonly IEmailConfirmTokenRepository _emailConfirmTokenRepository;
    private readonly EmailOptions _emailOptions;

    public EmailConfirmationUriCommandHandler(ITokensProvider tokensProvider,
        IEmailConfirmTokenRepository emailConfirmTokenRepository, IOptions<EmailOptions> emailOptions)
    {
        _tokensProvider = tokensProvider;
        _emailConfirmTokenRepository = emailConfirmTokenRepository;
        _emailOptions = emailOptions.Value;
    }

    public async Task<string> Handle(EmailConfirmationUriCommand request, CancellationToken cancellationToken)
    {
        var token = _tokensProvider.GenerateEmailConfirmationTokenAsync(request.Email);
        await _emailConfirmTokenRepository.AddOrUpdateAsync(token, cancellationToken);

        return string.Concat(_emailOptions.EmailConfirmationBaseUri, token.Token);
    }
}