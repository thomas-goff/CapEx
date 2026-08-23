using CapEx.Data;
using CapEx.Models;
using Microsoft.EntityFrameworkCore;

namespace CapEx.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly IDbContextFactory<CapExDbContext> _contextFactory;

    public UserRepository(IDbContextFactory<CapExDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetByIdAsync(int userId, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);
    }
}
