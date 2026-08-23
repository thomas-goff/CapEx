namespace CapEx.Models;

public static class RequestStatusExtensions
{
    public static string ToDisplayName(this RequestStatus status) => status switch
    {
        RequestStatus.Pending => "Pending",
        RequestStatus.Approved => "Approved",
        RequestStatus.Rejected => "Rejected",
        _ => status.ToString()
    };

    public static string ToBadgeClass(this RequestStatus status) => status switch
    {
        RequestStatus.Pending => "bg-warning text-dark",
        RequestStatus.Approved => "bg-success",
        RequestStatus.Rejected => "bg-danger",
        _ => "bg-secondary"
    };
}
