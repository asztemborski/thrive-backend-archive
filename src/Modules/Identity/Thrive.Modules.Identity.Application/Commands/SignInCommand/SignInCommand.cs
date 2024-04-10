using MediatR;
using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Application.Commands.SignInCommand;

public sealed record SignInCommand(string Email, string Password) : IRequest<Tokens>;