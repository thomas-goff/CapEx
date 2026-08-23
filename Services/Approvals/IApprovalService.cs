namespace CapEx.Services.Approvals;

public interface IApprovalService
{
    Task<DecisionResult> SubmitDecisionAsync(
        SubmitDecisionCommand command,
        CancellationToken cancellationToken = default);
}
