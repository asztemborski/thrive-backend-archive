using System.Security.Authentication;
using MediatR;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Exceptions;
using Thrive.Modules.Identity.Domain.Repositories;

namespace Thrive.Modules.Identity.Application.Commands.SignInCommand;

internal sealed class SignInCommandHandler : IRequestHandler<SignInCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokensProvider _tokensProvider;
    private readonly IValueHasher _valueHasher;
    private readonly ITokensRequestStorage _tokensRequestStorage;

    public SignInCommandHandler(ITokensProvider tokensProvider, IUserRepository userRepository,
        ITokensRequestStorage tokensRequestStorage, IValueHasher valueHasher)
    {
        _tokensProvider = tokensProvider;
        _userRepository = userRepository;
        _tokensRequestStorage = tokensRequestStorage;
        _valueHasher = valueHasher;
    }

    public async Task Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
                   ?? throw new InvalidCredentialsException();

        if (!user.Email.IsConfirmed)
        {
            throw new InvalidCredentialsException();
        }

        if (!user.IsActive)
        {
            throw new InvalidCredentialException();
        }

        if (!_valueHasher.Verify(user.Password, request.Password))
        {
            throw new InvalidCredentialsException();
        }

        var tokens = await _tokensProvider.GenerateAccessAsync(user, cancellationToken);
        _tokensRequestStorage.SetTokens(request.Email, tokens);
    }
}