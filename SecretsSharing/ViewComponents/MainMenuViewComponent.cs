using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;

namespace SecretsSharing.ViewComponents;

public class MainMenuViewComponent : ViewComponent
{
    private readonly ICurrentUser currentUser;

    public MainMenuViewComponent(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var isLoggedIn = await currentUser.IsLoggedIn();
        return View("Index", isLoggedIn);
    }
}