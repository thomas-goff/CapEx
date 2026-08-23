using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed class AmountBasedApprovalTierPolicy : IApprovalTierPolicy
{
    private static readonly IReadOnlyList<ApprovalTier> Tiers = new[]
    {
        new ApprovalTier(100_000m, new[]
        {
            UserRole.DepartmentManager,
            UserRole.FinanceDirector,
            UserRole.CEO
        }),
        new ApprovalTier(25_000m, new[]
        {
            UserRole.DepartmentManager,
            UserRole.FinanceDirector
        }),
        new ApprovalTier(5_000m, new[]
        {
            UserRole.DepartmentManager
        }),
        new ApprovalTier(0m, Array.Empty<UserRole>())
    };

    public IReadOnlyList<UserRole> GetRequiredApprovers(decimal amount)
    {
        foreach (var tier in Tiers)
        {
            if (amount >= tier.MinimumAmount)
            {
                return tier.RequiredApprovers;
            }
        }

        return Array.Empty<UserRole>();
    }
}
