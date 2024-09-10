using FluentValidation;

namespace Thrive.Modules.Identity.Application.Commands.RefreshTokens;

public sealed class RefreshTokensCommandValidator : AbstractValidator<RefreshTokens.RefreshTokensCommand>
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