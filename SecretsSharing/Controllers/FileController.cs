using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.General;
using SecretsSharing.Middleware;
using SecretsSharing.Service;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class FileController : Controller
{
    private readonly ICurrentUser currentUser;

    public FileController(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/file")]
    public IActionResult Index()
    {
        return View("Index", new FileViewModel());
    }

    [HttpPost]
    [Route("/file")]
    public async Task<IActionResult> IndexSave()
    {
        if (Request.Form.Files.Count > 0)
        {
            var fileData = Request.Form.Files[0];
            {
                var userId = await currentUser.GetUserId() ?? 0;
                var fileName =
                    WebFile.GetWebFileName(fileData.FileName, WebFileStartPathConstant.WebFilePath, userId);
                await WebFile.UploadFile(fileName, fileData);
                return Redirect("/");
            }
        }
        
        return View("Index", new FileViewModel());
    }
}