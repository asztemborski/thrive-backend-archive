namespace Thrive.Modules.Collaboration.Application.Dtos;

public sealed record WorkspacesListDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}