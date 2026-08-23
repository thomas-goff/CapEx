namespace CapEx.Services.Approvals;

public sealed record SubmitDecisionCommand(
    int RequestId,
    int ActedByUserId,
    bool Approved,
    string? Comment);
