namespace Thrive.Modules.Identity.Application.Contracts;

public interface IConfirmationUriRequestStorage
{
    string? RetrieveUri(string email);
    void SetUri(string key, string confirmationUri);
}