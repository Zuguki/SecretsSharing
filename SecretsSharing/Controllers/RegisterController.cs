using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.Exceptions;
using SecretsSharing.DAL.Models;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteNotAuthorize]
public class RegisterController : Controller
{
    private readonly IAuth auth;
    private readonly IMapper mapper;

    public RegisterController(IAuth auth, IMapper mapper)
    {
        this.auth = auth;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("/register")]
    public async Task<IActionResult> Index()
    {
        return View("Index", new RegisterViewModel());
    }

    [HttpPost]
    [Route("/register")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await auth.Register(mapper.Map<RegisterViewModel, UserModel>(model));
                return Redirect("/");
            }
            catch (DuplicateEmailException e)
            {
                ModelState.TryAddModelError("Email", "Email exists");
            }
        }

        return View("Index", model);
    }
}