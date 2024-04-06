using Fleet.Modules.Identity.Application.DTOs;

namespace Fleet.Modules.Identity.Application.Contracts;

public interface ITokensRequestStorage
{
    Tokens? RetrieveTokens(string email);
    void SetTokens(string key, Tokens tokens);
}