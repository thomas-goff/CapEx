using CapEx.Models;

namespace CapEx.Services.Authentication;

public interface ICurrentUserService
{
    User? User { get; }

    bool IsAuthenticated { get; }

    void SignIn(User user);

    void SignOut();

    event Action? Changed;
}
