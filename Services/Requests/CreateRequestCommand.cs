namespace CapEx.Services.Requests;

public sealed record CreateRequestCommand(
    int RequestedByUserId,
    string Title,
    decimal Amount,
    string Motivation);
