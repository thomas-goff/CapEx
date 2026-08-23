using CapEx.Models;

namespace CapEx.Services.Authentication;

public sealed class CurrentUserService : ICurrentUserService
{
    public User? User { get; private set; }

    public bool IsAuthenticated => User is not null;

    public event Action? Changed;

    public void SignIn(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        User = user;
        Changed?.Invoke();
    }

    public void SignOut()
    {
        User = null;
        Changed?.Invoke();
    }
}
