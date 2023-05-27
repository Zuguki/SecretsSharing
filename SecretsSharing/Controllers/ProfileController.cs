using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.File;
using SecretsSharing.DAL.Models;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class ProfileController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IMapper mapper;
    private readonly IFile file;

    public ProfileController(ICurrentUser currentUser, IMapper mapper, IFile file)
    {
        this.currentUser = currentUser;
        this.mapper = mapper;
        this.file = file;
    }

    [HttpGet]
    [Route("/profile")]
    public async Task<IActionResult> Index()
    {
        var files = await currentUser.GetFiles();
        var fileViewModels = files.Select(model => mapper.Map<FileModel, FileViewModel>(model));
        return View("Index", fileViewModels);
    }

    [HttpPost]
    [Route("/delete/{methodName}/{dir1}/{dir2}/{fileName}")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> IndexSave([FromRoute] string methodName, [FromRoute] string dir1,
        [FromRoute] string dir2, [FromRoute] string fileName)
    {
        var path = "/" + string.Join('/', methodName, dir1, dir2, fileName);
        await file.DeleteByPath(path);
        return Redirect("/profile");
    }
}