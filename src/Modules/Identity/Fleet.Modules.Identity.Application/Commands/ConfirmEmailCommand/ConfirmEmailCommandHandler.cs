using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.Exceptions;
using Fleet.Modules.Identity.Domain.Repositories;
using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.ConfirmEmailCommand;

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
        var token = await _confirmTokenRepository.GetByTokenAsync(request.ConfirmationToken) ??
                    throw new InvalidEmailConfirmationToken();

        var user = await _userRepository.GetByEmailAsync(token.Email) ?? throw new InvalidEmailConfirmationToken();
        user.Email.Confirm();
        await _userRepository.UpdateAsync(user);
    }
}