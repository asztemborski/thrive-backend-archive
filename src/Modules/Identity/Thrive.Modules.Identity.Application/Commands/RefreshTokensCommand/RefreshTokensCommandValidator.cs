using FluentValidation;

namespace Thrive.Modules.Identity.Application.Commands.RefreshTokensCommand;

public sealed class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
{
    private const string TokenPattern = @"^[a-zA-Z0-9_-]+\.[a-zA-Z0-9_-]+\.[a-zA-Z0-9_-]+$";

    public RefreshTokensCommandValidator()
    {
        RuleFor(request => request.RefreshToken)
            .NotNull()
            .NotEmpty()
            .Matches(TokenPattern);
    }
}