using SecretsSharing.BL.General;
using SecretsSharing.BL.Session;
using SecretsSharing.DAL;
using SecretsSharing.DAL.File;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IDbSession dbSession;
    private readonly IWebCookie webCookie;
    private readonly IUserTokenDAL userTokenDal;
    private readonly IFileDAL fileDal;

    public CurrentUser(IDbSession dbSession, IWebCookie webCookie, IUserTokenDAL userTokenDal, IFileDAL fileDal)
    {
        this.dbSession = dbSession;
        this.webCookie = webCookie;
        this.userTokenDal = userTokenDal;
        this.fileDal = fileDal;
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

    public async Task<IEnumerable<FileModel>> GetFiles()
    {
        var userId = await GetUserId();
        if (userId is null)
            throw new Exception("Error");

        return await fileDal.Get((int) userId);
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