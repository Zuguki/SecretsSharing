using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();

    Task<int?> GetUserId();

    Task<IEnumerable<FileModel>> GetFiles();
}