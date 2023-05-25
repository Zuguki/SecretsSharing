using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.Exceptions;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteNotAuthorize]
public class LoginController : Controller
{
    private readonly IAuth auth;
    private readonly IMapper mapper;

    public LoginController(IAuth auth, IMapper mapper)
    {
        this.auth = auth;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("/login")]
    public async Task<IActionResult> Index()
    {
        return View("Index", new LoginViewModel());
    }

    [HttpPost]
    [Route("/login")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
                return Redirect("/");
            }
            catch (AuthorizationException e)
            {
                ModelState.TryAddModelError("Email", "Name or Email is invalid");
            }
        }

        return View("Index", model);
    }
}