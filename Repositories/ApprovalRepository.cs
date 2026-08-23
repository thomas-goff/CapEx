using CapEx.Data;
using CapEx.Models;
using Microsoft.EntityFrameworkCore;

namespace CapEx.Repositories;

public sealed class ApprovalRepository : IApprovalRepository
{
    private readonly IDbContextFactory<CapExDbContext> _contextFactory;

    public ApprovalRepository(IDbContextFactory<CapExDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Approval> RecordDecisionAsync(
        Approval approval,
        RequestStatus newStatus,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(approval);

        await using var context = await _contextFactory.CreateDbContextAsync(cancellationToken);

        var request = await context.Requests
            .FirstOrDefaultAsync(r => r.RequestId == approval.RequestId, cancellationToken)
            ?? throw new InvalidOperationException($"Request {approval.RequestId} no longer exists.");

        request.Status = newStatus;
        context.Approvals.Add(approval);

        await context.SaveChangesAsync(cancellationToken);

        return approval;
    }
}
