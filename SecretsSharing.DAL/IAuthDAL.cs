using SecretsSharing.DAL.Models;

namespace SecretsSharing.DAL;

public interface IAuthDAL
{
    Task<UserModel> GetUser(int id);
    Task<UserModel> GetUser(string email);
    Task<int> CreateUser(UserModel model);
}