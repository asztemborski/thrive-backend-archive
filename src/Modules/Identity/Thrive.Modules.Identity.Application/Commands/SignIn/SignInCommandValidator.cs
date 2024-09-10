using FluentValidation;

namespace Thrive.Modules.Identity.Application.Commands.SignIn;

public sealed class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(request => request.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();

        RuleFor(request => request.Password)
            .NotNull()
            .NotEmpty();
    }
}