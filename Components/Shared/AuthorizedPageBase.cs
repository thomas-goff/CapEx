using CapEx.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace CapEx.Components.Shared;

public abstract class AuthorizedPageBase : ComponentBase
{
    [Inject]
    protected ICurrentUserService CurrentUser { get; set; } = default!;

    [Inject]
    protected NavigationManager Navigation { get; set; } = default!;

    protected sealed override async Task OnInitializedAsync()
    {
        if (!CurrentUser.IsAuthenticated)
        {
            Navigation.NavigateTo("/login", replace: true);
            return;
        }

        await OnAuthorizedInitializedAsync();
    }

    protected virtual Task OnAuthorizedInitializedAsync() => Task.CompletedTask;
}
