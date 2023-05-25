using SecretsSharing.BL.General;
using SecretsSharing.BL.Session;

namespace SecretsSharing.BL.Auth;

public class CurrentUser : ICurrentUser
{
    private readonly IDbSession dbSession;
    private readonly IWebCookie webCookie;

    public CurrentUser(IDbSession dbSession, IWebCookie webCookie)
    {
        this.dbSession = dbSession;
        this.webCookie = webCookie;
    }

    public async Task<bool> IsLoggedIn()
    { 
        return await dbSession.IsLoggedIn();
    }

    public async Task<int?> GetUserId() =>
        await dbSession.GetUserId();
}