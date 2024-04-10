using MediatR;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Exceptions;
using Thrive.Modules.Identity.Domain.Repositories;

namespace Thrive.Modules.Identity.Application.Commands.ConfirmEmailCommand;

internal sealed class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IEmailConfirmTokenRepository _confirmTokenRepository;
    private readonly IUserRepository _userRepository;

    public ConfirmEmailCommandHandler(IEmailConfirmTokenRepository confirmTokenRepository,
        IUserRepository userRepository)
    {
        _confirmTokenRepository = confirmTokenRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var token = await _confirmTokenRepository.GetByTokenAsync(request.ConfirmationToken, cancellationToken) 
                    ?? throw ApplicationExceptions.InvalidEmailConfirmationToken();

        if (token.IsExpired)
        {
            throw ApplicationExceptions.InvalidEmailConfirmationToken();
        }

        var user = await _userRepository.GetByEmailAsync(token.Email, cancellationToken)
                   ?? throw ApplicationExceptions.InvalidEmailConfirmationToken();
        
        user.Email.Confirm();
        await _userRepository.UpdateAsync(cancellationToken);
    }
}