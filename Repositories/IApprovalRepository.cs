using CapEx.Models;

namespace CapEx.Repositories;

public interface IApprovalRepository
{
    Task<Approval> RecordDecisionAsync(
        Approval approval,
        RequestStatus newStatus,
        CancellationToken cancellationToken = default);
}
