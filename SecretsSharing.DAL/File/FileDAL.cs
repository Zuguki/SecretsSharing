using SecretsSharing.DAL.Models;

namespace SecretsSharing.DAL.File;

public class FileDAL : IFileDAL
{
    public async Task<IEnumerable<FileModel>> Get(int userId)
    {
        var sql = @"select FileId, UserId, FileName, FileContent, FilePath
                    from FileQueue
                    where UserId = @id";
        
        return await DbHelper.QueryAsync<FileModel>(sql, new {id = userId});
    }

    public async Task<int> Add(FileModel model)
    {
        var sql = @"insert into FileQueue(UserId, FileName, FileContent, FilePath)
                    values(@UserId, @FileName, @FileContent, @FilePath)
                    returning FileId";

        var result = await DbHelper.QueryAsync<int>(sql, model);
        return result.First();
    }

    public async Task Update(FileModel model)
    {
        var sql = @"update FileQueue
                    set FileName = @FileName,
                        FileContent = @FileContent,
                        FilePath = @FilePath
                    where FileId = @FileId";
        
        await DbHelper.QueryAsync<int>(sql, model);
    }

    public async Task DeleteByPath(string path)
    {
        var sql = @"delete from FileQueue
                where FilePath = @filePath";

        await DbHelper.QueryAsync<int>(sql, new {filePath = path});
    }
}