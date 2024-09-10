using Thrive.Modules.Collaboration.Domain.Workspace.Exceptions;

namespace Thrive.Modules.Collaboration.Domain.Workspace.Entities;

public sealed class WorkspaceThreads
{
    private readonly List<Thread> _threads = [];
    public IReadOnlyCollection<Thread> Threads => _threads.AsReadOnly();

    private readonly List<ThreadCategory> _categories = [];
    public IReadOnlyCollection<ThreadCategory> Categories => _categories.AsReadOnly();

    private readonly Guid _workspaceId;

    internal WorkspaceThreads(Guid workspaceId)
    {
        _workspaceId = workspaceId;
    }
    
    public void AddCategory(ThreadCategory category)
    {
        if (CategoryExists(category.Id))
        {
            throw new CategoryAlreadyExists(category.Id);
        }
        
        _categories.Add(category);
    }
    
    public void AddThread(string name, ThreadCategory category)
    {
        if (!CategoryExists(category.Id))
        {
            throw new CategoryDoesNotExist(category.Id);
        }
        
        var thread = new Thread(_workspaceId, name, category);
        _threads.Add(thread);
    }

    private bool CategoryExists(Guid id) => _categories.Exists(c => c.Id == id);
}