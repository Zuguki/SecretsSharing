using Microsoft.AspNetCore.Mvc;
using SecretsSharing.BL.File;
using SecretsSharing.BL.General;

namespace SecretsSharing.Controllers;

public class FileActionController : Controller
{
    private readonly IFile file;

    public FileActionController(IFile file)
    {
        this.file = file;
    }

    [HttpPost]
    [Route("/delete/{methodName}/{dir1}/{dir2}/{fileName}")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Delete([FromRoute] string methodName, [FromRoute] string dir1,
        [FromRoute] string dir2, [FromRoute] string fileName)
    {
        var path = "/" + string.Join('/', methodName, dir1, dir2, fileName);
        await file.DeleteByPath(path);
        return Redirect("/profile");
    }

    public async Task<FileResult> Download(string path, string fileName)
    {
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
    }

    [HttpGet]
    [Route("/download/{methodName}/{dir1}/{dir2}/{fileName}/{fileCustomName}")]
    public async Task<IActionResult> Download([FromRoute] string methodName, [FromRoute] string dir1,
        [FromRoute] string dir2, [FromRoute] string fileName, [FromRoute] string fileCustomName)
    {
        var path = "./wwwroot/" + string.Join('/', methodName, dir1, dir2, fileName);
        var fileModel = await file.GetFileByPath(Helpers.WebFileNameToFileName(path));
        if (fileModel is null)
            return NotFound();
        
        var downloadFileName = fileCustomName + Path.GetExtension(fileName);
        var fileBytes = await System.IO.File.ReadAllBytesAsync(path);
        if (fileModel.ShouldBeDeleted == true)
            await Delete(methodName, dir1, dir2, fileName);
        
        return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, downloadFileName);
    }
}