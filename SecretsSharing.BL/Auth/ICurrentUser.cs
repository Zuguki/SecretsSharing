namespace SecretsSharing.BL.Auth;

public interface ICurrentUser
{
    Task<bool> IsLoggedIn();

    Task<int?> GetUserId();
}