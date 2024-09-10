using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.Exceptions;
using Thrive.Modules.Identity.Shared.DTO;


namespace Thrive.Modules.Identity.Application.Services;

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor) => _httpContextAccessor = httpContextAccessor;
    
    public CurrentUserDto GetCurrentUser()
    {
        var userClaims = _httpContextAccessor.HttpContext?.User.Claims;

        if (userClaims is null)
        {
            throw new UnauthorizedException();
        }
        
        var claimsDict = userClaims.ToDictionary(c => c.Type, c => c.Value);
        claimsDict.TryGetValue(ClaimTypes.NameIdentifier, out var idClaim);
        claimsDict.TryGetValue(ClaimTypes.Name, out var nameClaim);
        claimsDict.TryGetValue(ClaimTypes.Email, out var emailClaim);

        if (idClaim is null || nameClaim is null || emailClaim is null)
        {
            throw new UnauthorizedException();
        }

        return new CurrentUserDto
        {
            Id = Guid.Parse(idClaim),
            Username = nameClaim,
            Email = emailClaim
        };
    }
}