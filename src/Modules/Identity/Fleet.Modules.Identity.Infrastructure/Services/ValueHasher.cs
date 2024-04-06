using Fleet.Modules.Identity.Application.Contracts;
using BC = BCrypt.Net.BCrypt;

namespace Fleet.Modules.Identity.Infrastructure.Services;

internal sealed class ValueHasher : IValueHasher
{
    public string Hash(string value) => BC.HashPassword(value);

    public bool Verify(string valueHash, string value) => BC.Verify(value, valueHash);
}