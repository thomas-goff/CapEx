namespace CapEx.Models;

public class Request
{
    public int RequestId { get; set; }

    public int RequestedByUserId { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Motivation { get; set; } = string.Empty;

    public DateTime CreatedUtc { get; set; }

    public RequestStatus Status { get; set; }

    public User? RequestedByUser { get; set; }

    public ICollection<Approval> Approvals { get; set; } = new List<Approval>();
}
