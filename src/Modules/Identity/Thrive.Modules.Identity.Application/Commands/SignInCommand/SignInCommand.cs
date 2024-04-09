using MediatR;

namespace Thrive.Modules.Identity.Application.Commands.SignInCommand;

public sealed record SignInCommand(string Email, string Password) : IRequest;