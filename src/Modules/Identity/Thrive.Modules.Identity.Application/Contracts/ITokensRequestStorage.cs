using Thrive.Modules.Identity.Application.DTOs;

namespace Thrive.Modules.Identity.Application.Contracts;

public interface ITokensRequestStorage
{
    Tokens? RetrieveTokens(string email);
    void SetTokens(string key, Tokens tokens);
}