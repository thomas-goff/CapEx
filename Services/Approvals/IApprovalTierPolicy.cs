using CapEx.Models;

namespace CapEx.Services.Approvals;

public interface IApprovalTierPolicy
{
    IReadOnlyList<UserRole> GetRequiredApprovers(decimal amount);
}
