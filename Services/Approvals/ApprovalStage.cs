using CapEx.Models;

namespace CapEx.Services.Approvals;

public enum ApprovalStageState
{
    NotStarted = 0,

    AwaitingDecision = 1,

    Approved = 2,

    Rejected = 3
}

public sealed record ApprovalStage(UserRole Role, ApprovalStageState State, Approval? Action);
