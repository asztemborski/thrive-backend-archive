using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.DTOs;
using Thrive.Modules.Identity.Application.Options;
using Thrive.Modules.Identity.Domain.Entities;
using Thrive.Modules.Identity.Domain.Repositories;
using Thrive.Modules.Identity.Infrastructure.Exceptions;
using Thrive.Modules.Identity.Infrastructure.Options;

namespace Thrive.Modules.Identity.Infrastructure.Services;

internal sealed class TokensProvider : ITokensProvider
{
    private readonly IUserRepository _userRepository;
    private readonly JwtOptions _jwtOptions;
    private readonly JwtBearerOptions _jwtBearerOptions;
    private readonly EmailOptions _emailOptions;

    private const int MinutesInDay = 24 * 60;

    public TokensProvider(IUserRepository userRepository, IOptions<JwtOptions> jwtSettings,
        IOptions<JwtBearerOptions> jwtBearerOptions, IOptions<EmailOptions> emailOptions)
    {
        _userRepository = userRepository;
        _jwtBearerOptions = jwtBearerOptions.Value;
        _jwtOptions = jwtSettings.Value;
        _emailOptions = emailOptions.Value;
    }

    public async Task<Tokens> GenerateAccessAsync(IdentityUser identityUser,
        CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, identityUser.Id.ToString()),
            new(ClaimTypes.Email, identityUser.Email.Address),
            new(ClaimTypes.Name, identityUser.Username),
        };

        var accessToken = GenerateEncryptedToken(claims, _jwtOptions.TokenExpirationInMinutes);
        var refreshToken = GenerateEncryptedToken(claims,
            _jwtOptions.RefreshTokenExpirationInDays * MinutesInDay);
        var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenExpirationInDays);

        identityUser.AddRefreshToken(new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = refreshTokenExpirationDate,
            CreatedAt = DateTime.UtcNow
        });

        await _userRepository.UpdateAsync(cancellationToken);
        return new Tokens(accessToken, refreshToken, refreshTokenExpirationDate);
    }
    
    public async Task<Tokens> RefreshAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var principal = new JwtSecurityTokenHandler().ValidateToken(refreshToken,
            _jwtBearerOptions.TokenValidationParameters, out var validatedToken);

        var isValidGuid = Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out var userId);

        if (!isValidGuid || validatedToken is null)
        {
            throw InfrastructureExceptions.UnauthorizedException();
        }

        var user = await _userRepository.GetWithRefreshTokensAsync(userId, cancellationToken) 
                   ?? throw InfrastructureExceptions.UnauthorizedException();
        var associatedRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);

        if (associatedRefreshToken is null || associatedRefreshToken.IsExpired)
        {
            throw InfrastructureExceptions.UnauthorizedException();
        }

        user.RevokeRefreshToken(associatedRefreshToken);
        return await GenerateAccessAsync(user, cancellationToken);
    }

    public EmailConfirmationToken GenerateEmailConfirmationTokenAsync(string email)
    {
        var token = GenerateEncryptedToken([], _emailOptions.EmailConfirmationTokenExpirationTime);
        var expiresAt = DateTime.UtcNow.AddMinutes(_emailOptions.EmailConfirmationTokenExpirationTime);

        return new EmailConfirmationToken(email, token, expiresAt);
    }

    private string GenerateEncryptedToken(IEnumerable<Claim> claims, int expirationValueInMinutes)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(expirationValueInMinutes),
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}