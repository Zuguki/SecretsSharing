using SecretsSharing.DAL.Models;

namespace SecretsSharing.DAL;

public class AuthDAL : IAuthDAL
{
    public async Task<UserModel> GetUser(int id)
    {
        var sql = @"select UserId, Email, Password, Salt, Status
                    from AppUser
                    where UserId = @id";
        var users = await DbHelper.QueryAsync<UserModel>(sql, new {id});
        return users.FirstOrDefault() ?? new UserModel();
    }

    public async Task<UserModel> GetUser(string email)
    {
        var sql = @"select UserId, Email, Password, Salt, Status
                    from AppUser
                    where Email = @email";
        var users = await DbHelper.QueryAsync<UserModel>(sql, new {email});
        return users.FirstOrDefault() ?? new UserModel();
    }

    public async Task<int> CreateUser(UserModel model)
    {
        var sql = @"insert into AppUser(Email, Password, Salt, Status)
                    values(@Email, @Password, @Salt, @Status) returning UserId";
        return await DbHelper.QueryScalarAsync<int>(sql, model);
    }
}