using SecretsSharing.BL.Security;
using SecretsSharing.DAL;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public class Auth : IAuth
{
    private readonly IAuthDAL authDal;
    private readonly IEncrypt encrypt;

    public Auth(IAuthDAL authDal, IEncrypt encrypt)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
    }
    
    public async Task<int> CreateUser(UserModel model)
    {
        model.Salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, model.Salt);
        
        var id = await authDal.CreateUser(model);
        Login(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUser(email);
        if (user.Password == encrypt.HashPassword(password, user.Salt))
        {
            Login(user.UserId ?? 0);
            return user.UserId ?? 0;
        }
        return 0;
    }

    public void Login(int id)
    {
        Console.WriteLine($"User with id: {id} was logged in");
    }
}