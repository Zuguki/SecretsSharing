using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.DAL.Models;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

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
    public async Task<IActionResult> IndexSave(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var errorMessage = await auth.ValidateEmail(model.Email ?? "");
            if (errorMessage is not null)
                ModelState.TryAddModelError("Email", errorMessage.ErrorMessage!);
        }

        if (ModelState.IsValid)
        {
            await auth.CreateUser(mapper.Map<RegisterViewModel, UserModel>(model));
            return Redirect("/");
        }

        return View("Index", model);
    }
}