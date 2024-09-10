using Thrive.Modules.Collaboration.Domain.Workspace.ValueObjects;
using Thrive.Shared.Abstractions.Domain;

namespace Thrive.Modules.Collaboration.Domain.Workspace.Events;

public sealed record WorkspaceCreatedDomainEvent(Entities.Workspace Workspace, Owner Owner) : IDomainEvent;