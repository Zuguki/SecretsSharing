using SecretsSharing.BL.Exceptions;
using SecretsSharing.BL.General;
using SecretsSharing.BL.Security;
using SecretsSharing.BL.Session;
using SecretsSharing.DAL;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public class Auth : IAuth
{
    private readonly IAuthDAL authDal;
    private readonly IEncrypt encrypt;
    private readonly IDbSession dbSession;
    private readonly IUserTokenDAL userTokenDal;
    private readonly IWebCookie webCookie;

    public Auth(IAuthDAL authDal, IEncrypt encrypt, IDbSession dbSession, IUserTokenDAL userTokenDal, IWebCookie webCookie)
    {
        this.authDal = authDal;
        this.encrypt = encrypt;
        this.dbSession = dbSession;
        this.userTokenDal = userTokenDal;
        this.webCookie = webCookie;
    }

    public async Task<int> CreateUser(UserModel model)
    {
        model.Salt = Guid.NewGuid().ToString();
        model.Password = encrypt.HashPassword(model.Password, model.Salt);
        
        var id = await authDal.CreateUser(model);
        await Login(id);
        return id;
    }

    public async Task<int> Authenticate(string email, string password, bool rememberMe)
    {
        var user = await authDal.GetUser(email);
        if (user.UserId is not null && user.Password == encrypt.HashPassword(password, user.Salt))
        {
            await Login(user.UserId ?? 0);
            
            if (rememberMe)
            {
                var tokenId = await userTokenDal.Create(user.UserId ?? 0);
                webCookie.AddSecure(AuthConstants.RememberMeCookieName, tokenId.ToString(), AuthConstants.RememberMeDays);
            }
            
            return user.UserId ?? 0;
        }

        throw new AuthorizationException();
    }

    public async Task ValidateEmail(string email)
    {
        var user = await authDal.GetUser(email);
        if (user.UserId is not null)
            throw new DuplicateEmailException();
    }

    public async Task Register(UserModel model)
    {
        using (var scope = Helpers.CreateTransactionScope())
        {
            await dbSession.Lock();
            await ValidateEmail(model.Email);
            await CreateUser(model);
            scope.Complete();
        }
    }

    public async Task Login(int id)
    {
        await dbSession.SetUserId(id);
    }
}