using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.File;
using SecretsSharing.BL.General;
using SecretsSharing.DAL.Models;
using SecretsSharing.General;
using SecretsSharing.Middleware;
using SecretsSharing.Service;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class TextController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IFile file;
    private readonly IMapper mapper;

    public TextController(ICurrentUser currentUser, IFile file, IMapper mapper)
    {
        this.currentUser = currentUser;
        this.file = file;
        this.mapper = mapper;
    }


    [HttpGet]
    [Route("/text")]
    public IActionResult Index()
    {
        return View("Index", new FileViewModel());
    }

    [HttpPost]
    [Route("/text")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index(FileViewModel model)
    {
        var userId = await currentUser.GetUserId();
        if (userId is null)
            throw new Exception("Error");
        
        if (model.FileContent is not null && model.FileName is not null && ModelState.IsValid)
        {
            var fileModel = mapper.Map<FileViewModel, FileModel>(model);
            fileModel.UserId = (int) userId;
            var fileName =
                WebFile.GetWebFileName(model.FileName + ".txt", WebFileStartPathConstant.WebFileTextPath, (int) userId);
            var inputBytes = Encoding.ASCII.GetBytes(model.FileContent);
            await WebFile.UploadText(fileName, inputBytes);
            
            fileModel.FilePath = Helpers.WebFileNameToFileName(fileName);
            await file.AddOrUpdate(fileModel);
            
            return Redirect("/");
        }
        
        return View("Index", new FileViewModel());
    }
}