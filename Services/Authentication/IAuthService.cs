using CapEx.Models;

namespace CapEx.Services.Authentication;

public interface IAuthService
{
    Task<User?> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
}
