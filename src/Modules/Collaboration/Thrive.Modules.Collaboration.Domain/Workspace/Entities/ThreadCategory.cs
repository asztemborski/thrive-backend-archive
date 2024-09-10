namespace Thrive.Modules.Collaboration.Domain.Workspace.Entities;

public sealed record ThreadCategory
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; init; } = string.Empty;
    public int Index { get; init; }
}