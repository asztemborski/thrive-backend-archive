using Thrive.Shared.Abstractions.Domain;

namespace Thrive.Modules.Collaboration.Domain.Member.Entities;

public sealed class Member : AggregateRoot<Guid>
{
    public string Name { get; private set; } = string.Empty;
    public Guid WorkspaceId { get; } = Guid.Empty;
    
    public Member(Guid id, string name, Guid workspaceId)
    {
        Id = id;
        Name = name;
        WorkspaceId = workspaceId;
    }
    
    private Member() {}
}