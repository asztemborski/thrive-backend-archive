using System.Threading;
using System.Threading.Tasks;

namespace Thrive.Modules.Identity.Shared;

public interface IIdentityModuleApi
{
    Task<string?> GenerateEmailConfirmationUri(string email, CancellationToken cancellationToken);
}