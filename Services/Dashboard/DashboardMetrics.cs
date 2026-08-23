namespace CapEx.Services.Dashboard;

public sealed record DashboardMetrics(
    int TotalRequests,
    int PendingCount,
    int ApprovedCount,
    int RejectedCount,
    decimal TotalValue,
    decimal ApprovedValue,
    decimal PendingValue,
    int AwaitingYourDecision)
{
    public static DashboardMetrics Empty { get; } = new(0, 0, 0, 0, 0m, 0m, 0m, 0);
}
