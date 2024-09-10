using Thrive.Shared.Abstractions.Exceptions;

namespace Thrive.Modules.Collaboration.Domain.Workspace.Exceptions;

internal sealed class CategoryDoesNotExist(Guid guid) 
    : BaseException($"Category with id: ${guid} does not exist", ExceptionCodes.CategoryDoesNotExist);

internal sealed class CategoryAlreadyExists(Guid id) 
    : BaseException($"Category with id: ${id} already exists", ExceptionCodes.CategoryAlreadyExists);

internal sealed class MemberAlreadyExists(Guid id) 
    : BaseException($"Member with id: ${id} already exists", ExceptionCodes.MemberAlreadyExists);