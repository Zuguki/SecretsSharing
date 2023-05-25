using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SecretsSharing.BL.Auth;

namespace SecretsSharing.Middleware;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class SiteNotAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var currentUser = context.HttpContext.RequestServices.GetService<ICurrentUser>();
        if (currentUser is null)
            throw new Exception("No user middleware");

        var isLoggedIn = await currentUser.IsLoggedIn();
        if (isLoggedIn)
            context.Result = new RedirectResult("/");
    }
}