using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Session;

public interface IDbSession
{
    Task<SessionModel> GetSession();
    
    Task SetUserId(int userId);
    
    Task<int?> GetUserId();
    
    Task<bool> IsLoggedIn();
    
    Task Lock();

    void ResetSessionCache();
}