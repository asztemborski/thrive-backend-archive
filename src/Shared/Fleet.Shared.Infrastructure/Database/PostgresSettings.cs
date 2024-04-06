namespace Fleet.Shared.Infrastructure.Database;

internal sealed record PostgresSettings
{
    public string ConnectionString { get; init; } = string.Empty;
}