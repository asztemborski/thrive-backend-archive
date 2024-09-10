using MediatR;
using Thrive.Modules.Collaboration.Domain.Member.Entities;
using Thrive.Modules.Collaboration.Domain.Member.Repositories;
using Thrive.Modules.Collaboration.Domain.Workspace.Events;

namespace Thrive.Modules.Collaboration.Application.Events.Handlers;

internal sealed class WorkspaceCreatedEventHandler : INotificationHandler<WorkspaceCreatedDomainEvent>
{
    private readonly IMemberRepository _memberRepository;

    public WorkspaceCreatedEventHandler(IMemberRepository memberRepository) =>_memberRepository = memberRepository;
    
    public async Task Handle(WorkspaceCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var (id, name) = notification.Owner;
        var member = new Member(id, name, notification.Workspace.Id);
        await _memberRepository.AddAsync(member);
    }
}