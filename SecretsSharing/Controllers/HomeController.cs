using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.Session;
using SecretsSharing.Models;

namespace SecretsSharing.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICurrentUser currentUser;

    public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
    {
        _logger = logger;
        this.currentUser = currentUser;
    }

    public async Task<IActionResult> Index()
    {
        await currentUser.IsLoggedIn();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}