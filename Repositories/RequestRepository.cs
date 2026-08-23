using CapEx.Data;
using CapEx.Models;
using Microsoft.EntityFrameworkCore;

namespace CapEx.Repositories;

public sealed class RequestRepository : IRequestRepository
{
    private readonly IDbContextFactory<CapExDbContext> _contextFactory;

    public RequestRepository(IDbContextFactory<CapExDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<Request>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Requests
            .AsNoTracking()
            .Include(r => r.RequestedByUser)
            .Include(r => r.Approvals.OrderBy(a => a.CreatedUtc))
                .ThenInclude(a => a.ActedByUser)
            .OrderBy(r => r.RequestId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Request?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        return await context.Requests
            .AsNoTracking()
            .Include(r => r.RequestedByUser)
            .Include(r => r.Approvals.OrderBy(a => a.CreatedUtc))
                .ThenInclude(a => a.ActedByUser)
            .FirstOrDefaultAsync(r => r.RequestId == requestId, cancellationToken);
    }

    public async Task<Request> AddAsync(Request request, CancellationToken cancellationToken = default)
    {
        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        context.Requests.Add(request);

        await context.SaveChangesAsync(cancellationToken);

        return request;
    }
}
