using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.BL.File;
using SecretsSharing.DAL.Models;
using SecretsSharing.General;
using SecretsSharing.Middleware;
using SecretsSharing.Service;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class FileController : Controller
{
    private readonly ICurrentUser currentUser;
    private readonly IFile file;
    private readonly IMapper mapper;

    public FileController(ICurrentUser currentUser, IFile file, IMapper mapper)
    {
        this.currentUser = currentUser;
        this.file = file;
        this.mapper = mapper;
    }

    [HttpGet]
    [Route("/file")]
    public async Task<IActionResult> Index()
    {
        var files = await currentUser.GetFiles();
        var first = files.FirstOrDefault() ?? new FileModel();
        
        return View("Index", mapper.Map<FileModel, FileViewModel>(first));
    }

    [HttpPost]
    [Route("/file")]
    public async Task<IActionResult> IndexSave(FileViewModel model)
    {
        var files = await currentUser.GetFiles();
        var userId = await currentUser.GetUserId() ?? 0;
        if (model.FileId is not null && files.All(m => m.FileId != model.FileId))
            throw new Exception("Error");

        if (ModelState.IsValid)
        {
            var fileModel = mapper.Map<FileViewModel, FileModel>(model);
            fileModel.UserId = (int) userId;
            if (Request.Form.Files.Count > 0 && Request.Form.Files[0] is not null)
            {
                var fileData = Request.Form.Files[0];
                var fileName =
                    WebFile.GetWebFileName(fileData.FileName, WebFileStartPathConstant.WebFilePath, (int) userId);
                await WebFile.UploadFile(fileName, fileData);
                fileModel.FilePath = fileName;
            }

            await file.AddOrUpdate(fileModel);
            return Redirect("/");
        }

        return View("Index", new FileViewModel());
    }
}