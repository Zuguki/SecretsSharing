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
        if (model.FileId == null)
            await fileDal.Add(model);
        else
            await fileDal.Update(model);
    }
}