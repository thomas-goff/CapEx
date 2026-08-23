namespace CapEx.Models;

public class Approval
{
    public int ApprovalId { get; set; }

    public int RequestId { get; set; }

    public int ActedByUserId { get; set; }

    public bool Approved { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedUtc { get; set; }

    public Request? Request { get; set; }

    public User? ActedByUser { get; set; }
}
