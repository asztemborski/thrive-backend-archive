using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.Options;
using MediatR;
using Microsoft.Extensions.Options;

namespace Fleet.Modules.Identity.Application.Commands.EmailConfirmationUriCommand;

internal sealed class EmailConfirmationUriCommandHandler : IRequestHandler<EmailConfirmationUriCommand>
{
    private readonly ITokensProvider _tokensProvider;
    private readonly IEmailConfirmTokenRepository _emailConfirmTokenRepository;
    private readonly EmailOptions _emailOptions;
    private readonly IConfirmationUriRequestStorage _confirmationUriRequestStorage;

    public EmailConfirmationUriCommandHandler(ITokensProvider tokensProvider,
        IEmailConfirmTokenRepository emailConfirmTokenRepository, IOptions<EmailOptions> emailOptions,
        IConfirmationUriRequestStorage confirmationUriRequestStorage)
    {
        _tokensProvider = tokensProvider;
        _emailConfirmTokenRepository = emailConfirmTokenRepository;
        _confirmationUriRequestStorage = confirmationUriRequestStorage;
        _emailOptions = emailOptions.Value;
    }

    public async Task Handle(EmailConfirmationUriCommand request, CancellationToken cancellationToken)
    {
        var token = _tokensProvider.GenerateEmailConfirmationTokenAsync(request.Email);
        await _emailConfirmTokenRepository.AddOrUpdateAsync(token);

        var confirmationUri = string.Concat(_emailOptions.EmailConfirmationBaseUri, token.Token);
        _confirmationUriRequestStorage.SetUri(request.Email, confirmationUri);
    }
}