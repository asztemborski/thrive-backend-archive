using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.DTOs;
using Fleet.Modules.Identity.Application.Exceptions;
using Fleet.Modules.Identity.Domain.Entities;
using Fleet.Modules.Identity.Domain.Repositories;
using Fleet.Modules.Identity.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fleet.Modules.Identity.Infrastructure.Services;

internal sealed class TokensProvider : ITokensProvider
{
    private readonly IUserRepository _userRepository;
    private readonly JwtOptions _jwtOptions;
    private readonly JwtBearerOptions _jwtBearerOptions;

    private const int MinutesInDay = 24 * 60;

    public TokensProvider(IUserRepository userRepository, IOptions<JwtOptions> jwtSettings,
        IOptions<JwtBearerOptions> jwtBearerOptions)
    {
        _userRepository = userRepository;
        _jwtBearerOptions = jwtBearerOptions.Value;
        _jwtOptions = jwtSettings.Value;
    }

    public async Task<Tokens> GenerateAsync(IdentityUser identityUser)
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

        await _userRepository.UpdateAsync(identityUser);
        return new Tokens(accessToken, refreshToken, refreshTokenExpirationDate);
    }


    public async Task<Tokens> RefreshAsync(string refreshToken)
    {
        var principal = new JwtSecurityTokenHandler().ValidateToken(refreshToken,
            _jwtBearerOptions.TokenValidationParameters, out var validatedToken);

        var isValidGuid = Guid.TryParse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value,
            out var userId);

        if (!isValidGuid || validatedToken is null) throw new UnauthorizedException();

        var user = await _userRepository.GetWithRefreshTokensAsync(userId) ?? throw new UnauthorizedException();
        var associatedRefreshToken = user.RefreshTokens.FirstOrDefault(r => r.Token == refreshToken);

        if (associatedRefreshToken is null || associatedRefreshToken.IsExpired)
        {
            throw new UnauthorizedException();
        }

        user.RevokeRefreshToken(associatedRefreshToken);
        return await GenerateAsync(user);
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