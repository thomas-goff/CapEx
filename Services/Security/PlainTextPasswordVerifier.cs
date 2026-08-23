namespace CapEx.Services.Security;

public sealed class PlainTextPasswordVerifier : IPasswordVerifier
{
    public bool Verify(string providedPassword, string storedPassword)
    {
        //Deliberately kept simple, no hashing or salting for demonstration purposes.
        //In a real application, you should never store passwords in plain text.
        return string.Equals(providedPassword, storedPassword, StringComparison.Ordinal);
    }
}
