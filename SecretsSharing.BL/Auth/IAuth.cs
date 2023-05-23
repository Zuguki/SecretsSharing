using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public interface IAuth
{
    Task<int> CreateUser(UserModel model);
}