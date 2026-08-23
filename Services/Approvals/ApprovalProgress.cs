using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed record ApprovalProgress(
    IReadOnlyList<ApprovalStage> Stages,
    UserRole? NextApproverRole)
{
    public int TotalStages => Stages.Count;

    public int CompletedStages => Stages.Count(stage => stage.State == ApprovalStageState.Approved);

    public bool NeedsNoApproval => Stages.Count == 0;
}
