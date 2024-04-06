using MediatR;

namespace Fleet.Modules.Identity.Application.Commands.SignInCommand;

public sealed record SignInCommand(string Email, string Password) : IRequest;
