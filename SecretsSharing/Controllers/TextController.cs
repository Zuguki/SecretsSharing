using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class TextController : Controller
{
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
        if (model.Text is not null)
        {
            var md5Hash = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(model.Text);
            var hashBytes = md5Hash.ComputeHash(inputBytes);
            var hash = Convert.ToHexString(hashBytes);

            var dir = "./wwwroot/text/" + hash[..2] + "/" + hash[..4];
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var fileName = dir + "/" + model.Title + ".txt";

            using (var stream = System.IO.File.Create(fileName))
            {
                await stream.WriteAsync(inputBytes);
            }

            return Redirect("/");
        }
        
        return View("Index", new TextViewModel());
    }
}