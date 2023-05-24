using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

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
    public async Task<IActionResult> IndexSave(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            await auth.Authenticate(model.Email!, model.Password!, model.RememberMe == true);
            return Redirect("/");
        }

        return View("Index", model);
    }
}