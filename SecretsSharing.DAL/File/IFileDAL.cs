using SecretsSharing.DAL.Models;

namespace SecretsSharing.DAL.File;

public interface IFileDAL
{
    Task<IEnumerable<FileModel>> Get(int userId);

    Task<int> Add(FileModel model);

    Task Update(FileModel model);
    
    Task DeleteByPath(string path);
    Task<FileModel> GetFileByPath(string path);
}