using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Middleware;
using SecretsSharing.ViewModels;

namespace SecretsSharing.Controllers;

[SiteAuthorize]
public class FileController : Controller
{
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
            var imageData = Request.Form.Files[0];
            {
                var md5Hash = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(imageData.FileName);
                var hashBytes = md5Hash.ComputeHash(inputBytes);
                var hash = Convert.ToHexString(hashBytes);

                var dir = "./wwwroot/files/" + hash[..2] + "/" + hash[..4];
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var fileName = dir + "/" + imageData.FileName;

                using (var stream = System.IO.File.Create(fileName))
                {
                    await imageData.CopyToAsync(stream);
                }

                return Redirect("/");
            }
        }
        
        return View("Index", new FileViewModel());
    }
}