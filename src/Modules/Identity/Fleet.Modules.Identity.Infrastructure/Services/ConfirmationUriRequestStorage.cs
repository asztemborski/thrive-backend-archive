using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Shared.Abstractions.Storage;

namespace Fleet.Modules.Identity.Infrastructure.Services;

internal sealed class ConfirmationUriRequestStorage : IConfirmationUriRequestStorage {
    private readonly IRequestStorage _requestStorage;

    public ConfirmationUriRequestStorage(IRequestStorage requestStorage)
    {
        _requestStorage = requestStorage;
    }
    
    public string? RetrieveUri(string email)
    {
        return _requestStorage.Get<string>($"confirmationUri:{email}");
    }

    public void SetUri(string key, string confirmationUri)
    {
        _requestStorage.Set($"confirmationUri:{key}", confirmationUri);
    }
}