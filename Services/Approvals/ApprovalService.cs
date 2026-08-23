using CapEx.Models;
using CapEx.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CapEx.Services.Approvals;

public sealed class ApprovalService : IApprovalService
{
    private const int MaxCommentLength = 1000;

    private readonly IRequestRepository _requests;
    private readonly IUserRepository _users;
    private readonly IApprovalRepository _approvals;
    private readonly IApprovalWorkflow _workflow;

    public ApprovalService(
        IRequestRepository requests,
        IUserRepository users,
        IApprovalRepository approvals,
        IApprovalWorkflow workflow)
    {
        _requests = requests;
        _users = users;
        _approvals = approvals;
        _workflow = workflow;
    }

    public async Task<DecisionResult> SubmitDecisionAsync(
        SubmitDecisionCommand command,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command);

        var request = await _requests.GetByIdAsync(command.RequestId, cancellationToken);

        if (request is null)
        {
            return DecisionResult.Failure("That request no longer exists.");
        }

        var user = await _users.GetByIdAsync(command.ActedByUserId, cancellationToken);

        if (user is null)
        {
            return DecisionResult.Failure("Your user account could not be found.");
        }

        var eligibility = _workflow.GetEligibility(request, user);

        if (!eligibility.CanAct)
        {
            return DecisionResult.Failure(eligibility.Reason ?? "You cannot act on this request.");
        }

        var comment = NormaliseComment(command.Comment);

        if (comment is not null && comment.Length > MaxCommentLength)
        {
            return DecisionResult.Failure($"Comment cannot be longer than {MaxCommentLength} characters.");
        }

        var newStatus = _workflow.GetStatusAfterDecision(request, command.Approved);

        var approval = new Approval
        {
            RequestId = request.RequestId,
            ActedByUserId = user.UserId,
            Approved = command.Approved,
            Comment = comment
        };

        try
        {
            await _approvals.RecordDecisionAsync(approval, newStatus, cancellationToken);
        }
        catch (DbUpdateException)
        {
            return DecisionResult.Failure(
                "That decision was already recorded. Reopen the request to see where it stands.");
        }

        var updated = await _requests.GetByIdAsync(request.RequestId, cancellationToken);

        return updated is null
            ? DecisionResult.Failure("The decision was saved but the request could not be reloaded.")
            : DecisionResult.Success(updated);
    }

    private static string? NormaliseComment(string? comment)
        => string.IsNullOrWhiteSpace(comment) ? null : comment.Trim();
}
