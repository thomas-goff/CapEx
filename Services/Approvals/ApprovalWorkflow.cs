using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed class ApprovalWorkflow : IApprovalWorkflow
{
    private readonly IApprovalTierPolicy _tierPolicy;

    public ApprovalWorkflow(IApprovalTierPolicy tierPolicy)
    {
        _tierPolicy = tierPolicy;
    }

    public ApprovalProgress GetProgress(Request request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requiredRoles = _tierPolicy.GetRequiredApprovers(request.Amount);
        var actions = OrderedActions(request);

        var rejected = actions.Any(action => !action.Approved);
        var stages = new List<ApprovalStage>(requiredRoles.Count);

        for (var index = 0; index < requiredRoles.Count; index++)
        {
            var role = requiredRoles[index];

            if (index < actions.Count)
            {
                var action = actions[index];

                stages.Add(new ApprovalStage(
                    role,
                    action.Approved ? ApprovalStageState.Approved : ApprovalStageState.Rejected,
                    action));

                continue;
            }

            var state = !rejected && index == actions.Count
                ? ApprovalStageState.AwaitingDecision
                : ApprovalStageState.NotStarted;

            stages.Add(new ApprovalStage(role, state, null));
        }

        var nextRole = NextApproverRole(request, requiredRoles, actions.Count, rejected);

        return new ApprovalProgress(stages, nextRole);
    }

    public ApprovalEligibility GetEligibility(Request request, User user)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(user);

        if (request.Status != RequestStatus.Pending)
        {
            return ApprovalEligibility.Blocked(
                $"This request is already {request.Status.ToDisplayName().ToLowerInvariant()}.");
        }

        if (request.Approvals.Any(action => action.ActedByUserId == user.UserId))
        {
            return ApprovalEligibility.Blocked("You have already acted on this request.");
        }

        var progress = GetProgress(request);

        if (progress.NextApproverRole is not { } nextRole)
        {
            return ApprovalEligibility.Blocked("No approval is outstanding on this request.");
        }

        if (user.Role != nextRole)
        {
            return ApprovalEligibility.Blocked(
                $"Waiting on the {nextRole.ToDisplayName()}.");
        }

        return ApprovalEligibility.Allowed();
    }

    public RequestStatus GetStatusAfterDecision(Request request, bool approved)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!approved)
        {
            return RequestStatus.Rejected;
        }

        var requiredRoles = _tierPolicy.GetRequiredApprovers(request.Amount);
        var actionsAfterThisOne = OrderedActions(request).Count + 1;

        return actionsAfterThisOne >= requiredRoles.Count
            ? RequestStatus.Approved
            : RequestStatus.Pending;
    }

    private static List<Approval> OrderedActions(Request request)
        => request.Approvals
            .OrderBy(action => action.CreatedUtc)
            .ThenBy(action => action.ApprovalId)
            .ToList();

    private static UserRole? NextApproverRole(
        Request request,
        IReadOnlyList<UserRole> requiredRoles,
        int actionCount,
        bool rejected)
    {
        if (rejected || request.Status != RequestStatus.Pending)
        {
            return null;
        }

        return actionCount < requiredRoles.Count ? requiredRoles[actionCount] : null;
    }
}
