using MediatR;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.DTOs;
using Thrive.Modules.Identity.Application.Exceptions;
using Thrive.Modules.Identity.Domain.Repositories;

namespace Thrive.Modules.Identity.Application.Commands.SignIn;

internal sealed class SignInCommandHandler : IRequestHandler<SignInCommand, Tokens>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokensProvider _tokensProvider;
    private readonly IValueHasher _valueHasher;

    public SignInCommandHandler(ITokensProvider tokensProvider, IUserRepository userRepository, 
        IValueHasher valueHasher)
    {
        _tokensProvider = tokensProvider;
        _userRepository = userRepository;
        _valueHasher = valueHasher;
    }

    public async Task<Tokens> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken)
                   ?? throw new InvalidCredentialsException();

        if (!user.Email.IsConfirmed || !user.IsActive)
        {
            throw new InvalidCredentialsException();
        }
        
        if (!_valueHasher.Verify(user.Password, request.Password))
        {
            throw new InvalidCredentialsException();
        }

        return await _tokensProvider.GenerateAccessAsync(user, cancellationToken);
    }
}