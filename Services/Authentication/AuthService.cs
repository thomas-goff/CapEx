using CapEx.Repositories;
using CapEx.Services.Security;

namespace CapEx.Services.Authentication;

public sealed class AuthService : IAuthService
{
    private const string InvalidCredentials = "That email and password combination is not recognised.";

    private readonly IUserRepository _users;
    private readonly IPasswordVerifier _passwordVerifier;

    public AuthService(IUserRepository users, IPasswordVerifier passwordVerifier)
    {
        _users = users;
        _passwordVerifier = passwordVerifier;
    }

    public async Task<LoginResult> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return LoginResult.Failure(InvalidCredentials);
        }

        var user = await _users.GetByEmailAsync(email.Trim(), cancellationToken);

        if (user is null || !_passwordVerifier.Verify(password, user.Password))
        {
            return LoginResult.Failure(InvalidCredentials);
        }

        return LoginResult.Success(user);
    }
}
