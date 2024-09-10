namespace Thrive.Modules.Collaboration.Domain.Member.Repositories;

public interface IMemberRepository
{
    Task<Member.Entities.Member?> GetByIdAsync(Guid id);
    Task AddAsync(Entities.Member member);
}