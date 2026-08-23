using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed class AmountBasedApprovalTierPolicy : IApprovalTierPolicy
{
    private static readonly IReadOnlyList<ApprovalTier> Tiers =
    [
        new(100_000m, [UserRole.DepartmentManager, UserRole.FinanceDirector, UserRole.CEO]),
        new(25_000m, [UserRole.DepartmentManager, UserRole.FinanceDirector]),
        new(5_000m, [UserRole.DepartmentManager]),
        new(0m, [])
    ];

    public decimal ApprovalThreshold { get; } = Tiers
        .Where(tier => tier.RequiredApprovers.Count > 0)
        .Min(tier => tier.MinimumAmount);

    public IReadOnlyList<UserRole> GetRequiredApprovers(decimal amount)
        => Tiers.FirstOrDefault(tier => amount >= tier.MinimumAmount)?.RequiredApprovers ?? [];
}
