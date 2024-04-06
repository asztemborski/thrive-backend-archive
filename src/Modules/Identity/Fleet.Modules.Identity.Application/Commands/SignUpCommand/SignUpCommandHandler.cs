using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.Exceptions;
using Fleet.Modules.Identity.Domain.Entities;
using Fleet.Modules.Identity.Domain.Repositories;
using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.SignUpCommand;

public sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IValueHasher _valueHasher;

    public SignUpCommandHandler(IUserRepository userRepository, IValueHasher valueHasher)
    {
        _userRepository = userRepository;
        _valueHasher = valueHasher;
    }

    public async Task Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        if (!await _userRepository.IsEmailUniqueAsync(request.Email))
        {
            throw new EmailAlreadyUsedException(request.Email);
        }

        if (!await _userRepository.IsUsernameUniqueAsync(request.Username))
        {
            throw new UsernameAlreadyUsedException(request.Username);
        }

        var hashedPassword = _valueHasher.Hash(request.Password);
        var user = new IdentityUser(request.Email, request.Username, hashedPassword);
        await _userRepository.CreateAsync(user);
    }
}