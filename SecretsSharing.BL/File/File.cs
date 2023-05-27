using SecretsSharing.BL.General;
using SecretsSharing.DAL.File;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.File;

public class File : IFile
{
    private readonly IFileDAL fileDal;

    public File(IFileDAL fileDal)
    {
        this.fileDal = fileDal;
    }

    public async Task<IEnumerable<FileModel>> Get(int userId)
    {
        return await fileDal.Get(userId);
    }

    public async Task AddOrUpdate(FileModel model)
    {
        await fileDal.Add(model);
        // if (model.FileId == null)
        //     await fileDal.Add(model);
        // else
        //     await fileDal.Update(model);
    }

    public async Task DeleteByPath(string path)
    {
        var webFileName = Helpers.FileNameToWebFileName(path);
        if (System.IO.File.Exists(webFileName))
            System.IO.File.Delete(webFileName);
        await fileDal.DeleteByPath(path);
    }
}