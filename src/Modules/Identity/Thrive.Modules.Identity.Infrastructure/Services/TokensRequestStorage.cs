using Thrive.Modules.Identity.Application.Contracts;
using Thrive.Modules.Identity.Application.DTOs;
using Thrive.Shared.Abstractions.Storage;

namespace Thrive.Modules.Identity.Infrastructure.Services;

internal sealed class TokensRequestStorage : ITokensRequestStorage
{
    private readonly IRequestStorage _requestStorage;

    public TokensRequestStorage(IRequestStorage requestStorage) => _requestStorage = requestStorage;

    public Tokens? RetrieveTokens(string key) => _requestStorage.Get<Tokens>($"tokens:{key}");

    public void SetTokens(string key, Tokens tokens) => _requestStorage.Set($"tokens:${key}", tokens);
}