using Microsoft.EntityFrameworkCore;
using Thrive.Modules.Collaboration.Domain.Member.Entities;
using Thrive.Modules.Collaboration.Domain.Member.Repositories;

namespace Thrive.Modules.Collaboration.Infrastructure.Database.Repositories;

internal sealed class MemberRepository : IMemberRepository
{
    private readonly CollaborationContext _context;

    public MemberRepository(CollaborationContext context) => _context = context;
    
    public Task<Member?> GetByIdAsync(Guid id)
    {
        return _context.Members.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Member member)
    {
        await _context.Members.AddAsync(member);
        await _context.SaveChangesAsync();
    }
}