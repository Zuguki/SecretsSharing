using SecretsSharing.DAL;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public class Auth : IAuth
{
    private readonly IAuthDAL authDal;

    public Auth(IAuthDAL authDal)
    {
        this.authDal = authDal;
    }

    public async Task<int> CreateUser(UserModel model)
    {
        return await authDal.CreateUser(model);
    }
}