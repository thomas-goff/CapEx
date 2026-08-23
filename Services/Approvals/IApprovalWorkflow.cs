using CapEx.Models;

namespace CapEx.Services.Approvals;

public interface IApprovalWorkflow
{
    ApprovalProgress GetProgress(Request request);

    ApprovalEligibility GetEligibility(Request request, User user);

    RequestStatus GetStatusAfterDecision(Request request, bool approved);
}
