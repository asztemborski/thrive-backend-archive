using MediatR;
using Thrive.Modules.Collaboration.Application.Dtos;

namespace Thrive.Modules.Collaboration.Application.Queries.WorkspacesList;

public sealed record WorkspacesListQuery : IRequest<IEnumerable<WorkspacesListDto>>;