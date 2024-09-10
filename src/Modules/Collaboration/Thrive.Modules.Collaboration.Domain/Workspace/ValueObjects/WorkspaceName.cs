namespace Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;

public sealed record WorkspaceName
{
    public string Value { get; }

    private WorkspaceName(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception();
        }

        if (value.Length is < 1 or > 20)
        {
            throw new Exception();
        }

        Value = value;
    }
    
    public static implicit operator string(WorkspaceName workspaceName) => workspaceName.Value;

    public static implicit operator WorkspaceName(string workspaceName) => new(workspaceName);
}