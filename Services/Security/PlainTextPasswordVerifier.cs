namespace CapEx.Services.Security;

public sealed class PlainTextPasswordVerifier : IPasswordVerifier
{
    public bool Verify(string providedPassword, string storedPassword)
    {
        return string.Equals(providedPassword, storedPassword, StringComparison.Ordinal);
    }
}
