namespace Thrive.Modules.Collaboration.Domain.Workspace.Entities;

public sealed class Thread
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid WorkspaceId { get; private init; }
    public string Name { get; private set; } = string.Empty;
    public Guid CategoryId { get; private set; } 

    internal Thread(Guid workspaceId, string name, ThreadCategory category)
    {
        WorkspaceId = workspaceId;
        Name = name;
        CategoryId = category.Id;
    }
    
    private Thread() {}
}