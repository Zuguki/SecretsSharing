using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.File;

public interface IFile
{
    Task<IEnumerable<FileModel>> Get(int userId);
    Task AddOrUpdate(FileModel model);
    Task DeleteByPath(string path);
}