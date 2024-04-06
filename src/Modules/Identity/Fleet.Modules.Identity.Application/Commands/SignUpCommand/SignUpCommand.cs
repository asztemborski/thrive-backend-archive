using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.SignUpCommand;

public sealed record SignUpCommand(
    string Email,
    string Username,
    string Password,
    string ConfirmPassword) : IRequest;