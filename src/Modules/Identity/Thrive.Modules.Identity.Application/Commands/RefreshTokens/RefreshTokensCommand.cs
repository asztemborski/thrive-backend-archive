using MediatR;
using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Application.Commands.RefreshTokens;

public sealed record RefreshTokensCommand(string RefreshToken) : IRequest<Tokens>;