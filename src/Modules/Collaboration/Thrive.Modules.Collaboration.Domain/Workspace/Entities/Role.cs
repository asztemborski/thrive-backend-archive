using Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;

namespace Thrive.Modules.Collaboration.Domain.Workspace.Entities;

public sealed class Role
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Color { get; private set; }

    private readonly List<Permission> _permissions = [];
    public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();
  
    public int Priority { get; private set; }
    
    internal Role(string name, string color)
    {
        Name = name;
        Color = color;
    }

    public void AddPermission(Permission permission)
    {
        if (_permissions.Contains(permission))
        {
            throw new Exception();
        }
        
        _permissions.Add(permission);
    }
}