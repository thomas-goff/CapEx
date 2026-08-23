using CapEx.Models;

namespace CapEx.Services.Approvals;

public sealed record DecisionResult
{
    private DecisionResult(bool succeeded, Request? request, string? errorMessage)
    {
        Succeeded = succeeded;
        Request = request;
        ErrorMessage = errorMessage;
    }

    public bool Succeeded { get; }

    public Request? Request { get; }

    public string? ErrorMessage { get; }

    public static DecisionResult Success(Request request) => new(true, request, null);

    public static DecisionResult Failure(string errorMessage) => new(false, null, errorMessage);
}
