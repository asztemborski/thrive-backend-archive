using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.Exceptions;
using Fleet.Modules.Identity.Domain.Repositories;
using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.SignInCommand;

public sealed class SignInCommandHandler : IRequestHandler<SignInCommand>
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
        var user = await _userRepository.GetByEmailAsync(request.Email)
                   ?? throw new InvalidCredentialsException();

        if (!user.Email.IsConfirmed)
        {
            throw new InvalidCredentialsException();
        }

        if (!_valueHasher.Verify(user.Password, request.Password))
        {
            throw new InvalidCredentialsException();
        }

        var tokens = await _tokensProvider.GenerateAsync(user);
        _tokensRequestStorage.SetTokens(request.Email, tokens);
    }
}