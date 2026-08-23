namespace CapEx.Services.Approvals;

public sealed record ApprovalEligibility
{
    private ApprovalEligibility(bool canAct, string? reason)
    {
        CanAct = canAct;
        Reason = reason;
    }

    public bool CanAct { get; }

    public string? Reason { get; }

    public static ApprovalEligibility Allowed() => new(true, null);

    public static ApprovalEligibility Blocked(string reason) => new(false, reason);
}
