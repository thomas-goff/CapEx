using CapEx.Models;

namespace CapEx.Services.Dashboard;

public interface IDashboardMetricsCalculator
{
    DashboardMetrics Calculate(IReadOnlyCollection<Request> requests, User? viewer);
}
