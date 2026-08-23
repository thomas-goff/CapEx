using CapEx.Models;
using CapEx.Services.Approvals;

namespace CapEx.Services.Dashboard;

public sealed class DashboardMetricsCalculator : IDashboardMetricsCalculator
{
    private readonly IApprovalWorkflow _workflow;

    public DashboardMetricsCalculator(IApprovalWorkflow workflow)
    {
        _workflow = workflow;
    }

    public DashboardMetrics Calculate(IReadOnlyCollection<Request> requests, User? viewer)
    {
        ArgumentNullException.ThrowIfNull(requests);

        if (requests.Count == 0)
        {
            return DashboardMetrics.Empty;
        }

        var pending = requests.Where(r => r.Status == RequestStatus.Pending).ToList();
        var approved = requests.Where(r => r.Status == RequestStatus.Approved).ToList();

        return new DashboardMetrics(
            TotalRequests: requests.Count,
            PendingCount: pending.Count,
            ApprovedCount: approved.Count,
            RejectedCount: requests.Count(r => r.Status == RequestStatus.Rejected),
            TotalValue: requests.Sum(r => r.Amount),
            ApprovedValue: approved.Sum(r => r.Amount),
            PendingValue: pending.Sum(r => r.Amount),
            AwaitingYourDecision: CountAwaitingViewer(pending, viewer));
    }

    private int CountAwaitingViewer(IEnumerable<Request> pending, User? viewer)
    {
        if (viewer is null || !viewer.Role.IsApprover())
        {
            return 0;
        }

        return pending.Count(request => _workflow.GetEligibility(request, viewer).CanAct);
    }
}
