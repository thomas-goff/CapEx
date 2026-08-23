using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed record ApprovalTier(decimal MinimumAmount, IReadOnlyList<UserRole> RequiredApprovers);
