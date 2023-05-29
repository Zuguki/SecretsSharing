using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.General;
using SecretsSharing.Middleware;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class LogOutController : Controller
{
    private readonly IWebCookie webCookie;

    public LogOutController(IWebCookie webCookie)
    {
        this.webCookie = webCookie;
    }

    [HttpGet]
    [Route("/logout")]
    public IActionResult Index()
    {
        webCookie.Delete(AuthConstants.SessionCookieName);
        webCookie.Delete(AuthConstants.RememberMeCookieName);
        return Redirect("/");
    }
}