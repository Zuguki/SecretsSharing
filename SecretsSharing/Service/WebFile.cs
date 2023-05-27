using SecretsSharing.BL.General;

namespace SecretsSharing.Service;

public static class WebFile
{
    public static string GetWebFileName(string fileName, string startPath, int userId = 0)
    {
        var dir = GetWebFileFolder(fileName, startPath);
        CreateFolder(dir);
        var userIdHash = userId.ToString().StringToHashString();
        return dir + "/" + userIdHash[..2] + Guid.NewGuid() + Path.GetExtension(fileName);
    }

    public static async Task UploadFile(string fileName, IFormFile fileData)
    {
        await using var stream = File.Create(fileName);
        await fileData.CopyToAsync(stream);
    }

    public static async Task UploadText(string fileName, byte[] content)
    {
        await using var stream = File.Create(fileName);
        await stream.WriteAsync(content);
    }
    
    public static string GetWebFileFolder(string fileName, string startPath)
    {
        var hashFileName = fileName.StringToHashString();

        return startPath + hashFileName[..2] + "/" + hashFileName[..4];
    }

    public static void CreateFolder(string dir)
    {
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
    }


}