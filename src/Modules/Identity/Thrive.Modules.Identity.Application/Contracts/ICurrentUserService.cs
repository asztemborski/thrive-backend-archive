using Thrive.Modules.Identity.Shared.DTO;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface ICurrentUserService
{
    CurrentUserDto GetCurrentUser();
}