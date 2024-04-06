using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Fleet.Modules.Identity.Application.Commands.SignUpCommand;

public sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator(IOptions<IdentityOptions> identityOptions)
    {
        RuleFor(request => request.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .MinimumLength(5)
            .MaximumLength(100);

        RuleFor(request => request.Username)
            .NotEmpty()
            .NotNull();

        RuleFor(request => request.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(identityOptions.Value.Password.RequiredLength);

        RuleFor(request => request.ConfirmPassword)
            .NotEmpty()
            .NotNull()
            .Equal(request => request.Password)
            .WithMessage("Confirm Password' must be equal to 'Password'.");
    }
}