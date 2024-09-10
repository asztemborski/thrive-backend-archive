using MediatR;

namespace Thrive.Modules.Collaboration.Application.Commands.CreateWorkspaceCommand;

public sealed record CreateWorkspaceCommand(string Name, string Description) : IRequest;