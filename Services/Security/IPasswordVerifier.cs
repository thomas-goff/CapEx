namespace CapEx.Services.Security;

public interface IPasswordVerifier
{
    bool Verify(string providedPassword, string storedPassword);
}
