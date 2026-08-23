using CapEx.Models;

namespace CapEx.Services.Approvals;

public interface IApprovalTierPolicy
{
    decimal ApprovalThreshold { get; }

    IReadOnlyList<UserRole> GetRequiredApprovers(decimal amount);
}
