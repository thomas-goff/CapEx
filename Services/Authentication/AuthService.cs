using CapEx.Models;
using CapEx.Repositories;
using CapEx.Services.Security;

namespace CapEx.Services.Authentication;

public sealed class AuthService : IAuthService
{
    private readonly IUserRepository _users;
    private readonly IPasswordVerifier _passwordVerifier;

    public AuthService(IUserRepository users, IPasswordVerifier passwordVerifier)
    {
        _users = users;
        _passwordVerifier = passwordVerifier;
    }

    public async Task<User?> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        var user = await _users.GetByEmailAsync(email.Trim(), cancellationToken);

        if (user is null || !_passwordVerifier.Verify(password, user.Password))
        {
            return null;
        }

        return user;
    }
}
