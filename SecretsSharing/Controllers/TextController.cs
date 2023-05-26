using System.Text;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.Auth;
using SecretsSharing.General;
using SecretsSharing.Middleware;
using SecretsSharing.Service;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class TextController : Controller
{
    private readonly ICurrentUser currentUser;

    public TextController(ICurrentUser currentUser)
    {
        this.currentUser = currentUser;
    }

    [HttpGet]
    [Route("/text")]
    public IActionResult Index()
    {
        return View("Index", new TextViewModel());
    }

    [HttpPost]
    [Route("/text")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Index(TextViewModel model)
    {
        if (model.Text is not null && model.Title is not null)
        {
            var userId = await currentUser.GetUserId() ?? 0;
            var fileName =
                WebFile.GetWebFileName(model.Title + ".txt", WebFileStartPathConstant.WebFileTextPath, userId);
            var inputBytes = Encoding.ASCII.GetBytes(model.Text);
            await WebFile.UploadText(fileName, inputBytes);
            
            return Redirect("/");
        }
        
        return View("Index", new TextViewModel());
    }
}