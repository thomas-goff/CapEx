using CapEx.Models;

namespace CapEx.Services.Authentication;

public sealed record LoginResult
{
    private LoginResult(bool succeeded, User? user, string? errorMessage)
    {
        Succeeded = succeeded;
        User = user;
        ErrorMessage = errorMessage;
    }

    public bool Succeeded { get; }

    public User? User { get; }

    public string? ErrorMessage { get; }

    public static LoginResult Success(User user) => new(true, user, null);

    public static LoginResult Failure(string errorMessage) => new(false, null, errorMessage);
}
