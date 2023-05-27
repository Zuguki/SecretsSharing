using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.DAL.Models;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class ProfileController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IMapper mapper;

    public ProfileController(ICurrentUser currentUser, IMapper mapper)
    {
        this.currentUser = currentUser;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("/profile")]
    public async Task<IActionResult> Index()
    {
        var files = await currentUser.GetFiles();
        var fileViewModels = files.Select(model => mapper.Map<FileModel, FileViewModel>(model));
        return View("Index", fileViewModels);
    }
}