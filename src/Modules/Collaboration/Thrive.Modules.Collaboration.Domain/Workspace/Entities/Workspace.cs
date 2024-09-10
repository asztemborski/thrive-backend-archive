using Thrive.Modules.Collaboration.Domain.Workspace.Events;
using Thrive.Modules.Collaboration.Domain.Workspace.Exceptions;
using Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;
using Thrive.Shared.Abstractions.Domain;

namespace Thrive.Modules.Collaboration.Domain.Workspace.Entities;

public sealed class Workspace : AggregateRoot<Guid>
{
    public WorkspaceName Name { get; private set; } = null!;
    public string Description { get; private set; } = string.Empty;
    public WorkspaceThreads WorkspaceThreads { get; } = null!;
    
    private readonly List<Guid> _members = [];
    public IReadOnlyCollection<Guid> Members => _members.AsReadOnly();

    private readonly List<Role> _roles = [];
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
    
    public Guid OwnerId { get; private set; }
    
    public Workspace(WorkspaceName name, string description, Owner owner)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        OwnerId = owner.Id;

        WorkspaceThreads = new WorkspaceThreads(Id);
        AddEvent(new WorkspaceCreatedDomainEvent(this, owner));
    }

    private Workspace() { }

    public void AddMember(Guid memberId)
    {
        if (_members.Exists(m => m == memberId))
        {
            throw new MemberAlreadyExists(memberId);
        }
        
        _members.Add(memberId);
    }

    public void AddRole(string name, string color)
    {
        _roles.Add(new Role(name, color));
    }
}