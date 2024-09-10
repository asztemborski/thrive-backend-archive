using MediatR;
using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Application.Commands.SignIn;

public sealed record SignInCommand(string Email, string Password) : IRequest<Tokens>;