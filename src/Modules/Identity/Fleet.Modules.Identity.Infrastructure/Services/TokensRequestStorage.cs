using Fleet.Modules.Identity.Application.Contracts;
using Fleet.Modules.Identity.Application.DTOs;
using Fleet.Shared.Abstractions.Storage;

namespace Fleet.Modules.Identity.Infrastructure.Services;

internal sealed class TokensRequestStorage : ITokensRequestStorage
{
    private readonly IRequestStorage _requestStorage;

    public TokensRequestStorage(IRequestStorage requestStorage) => _requestStorage = requestStorage;

    public Tokens? RetrieveTokens(string key) => _requestStorage.Get<Tokens>($"tokens:{key}");

    public void SetTokens(string key, Tokens tokens) => _requestStorage.Set($"tokens:${key}", tokens);
}