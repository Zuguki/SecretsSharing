using SecretsSharing.BL.General;
using SecretsSharing.BL.Session;
using SecretsSharing.DAL;

namespace SecretsSharing.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IDbSession dbSession;
    private readonly IWebCookie webCookie;
    private readonly IUserTokenDAL userTokenDal;

    public CurrentUser(IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDal)
    {
        this.dbSession = dbSession;
        this.webCookie = webCookie;
        this.userTokenDal = userTokenDal;
    }

    public async Task<bool> IsLoggedIn()
    {
        var isLoggedIn = await dbSession.IsLoggedIn();
        if (!isLoggedIn)
        {
            var userId = await GetUserIdByToken();
            if (userId is not null && userId != -1)
            {
                await dbSession.SetUserId((int) userId);
                isLoggedIn = true;
            }
        }

        return isLoggedIn;
    }

    public async Task<int?> GetUserId() =>
        await dbSession.GetUserId();


    private async Task<int?> GetUserIdByToken()
    {
        var cookie = webCookie.Get(AuthConstants.RememberMeCookieName);
        if (cookie is null)
            return null;

        var userTokenId = Helpers.StringToGuidDef(cookie);
        if (userTokenId is null)
            return null;

        return await userTokenDal.Get((Guid) userTokenId);
    }
}