using SecretsSharing.BL.Auth;
using SecretsSharing.BL.General;
using SecretsSharing.DAL;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Session;

public class DbSession : IDbSession
{
    private readonly IDbSessionDAL sessionDal;
    private SessionModel? sessionModel = null;
    private readonly IWebCookie webCookie;

    public DbSession(IDbSessionDAL sessionDal, IWebCookie cookie)
    {
        this.sessionDal = sessionDal;
        webCookie = cookie;
    }

    public async Task<SessionModel> GetSession()
    {
        if (sessionModel is not null)
            return sessionModel;

        SessionModel? session = null;
        var cookieValue = webCookie.Get(AuthConstants.SessionCookieName);
        if (cookieValue is not null)
        {
            var sessionId = Guid.Parse(cookieValue);
            session = await sessionDal.Get(sessionId);
        }

        if (session is null)
        {
            session = await CreateSession();
            CreateSessionCookie(session.DbSessionId);
        }

        sessionModel = session;
        return session;
    }

    public async Task SetUserId(int userId)
    {
        var session = await GetSession();
        session.DbSessionId = Guid.NewGuid();
        session.UserId = userId;
        
        CreateSessionCookie(session.DbSessionId);
        await sessionDal.Create(session);
    }

    public async Task<int?> GetUserId()
    {
        var session = await GetSession();
        return session.UserId;
    }

    public async Task<bool> IsLoggedIn()
    {
        var session = await GetSession();
        return session.UserId is not null;
    }

    public async Task Lock()
    {
        var data = await GetSession();
        await sessionDal.Lock(data.DbSessionId);
    }

    public void ResetSessionCache()
    {
        sessionModel = null;
    }

    private async Task<SessionModel> CreateSession()
    {
        var session = new SessionModel()
        {
            DbSessionId = Guid.NewGuid(),
            Created = DateTime.Now,
            LastAccessed = DateTime.Now,
        };

        await sessionDal.Create(session);
        return session;
    }
    
    private void CreateSessionCookie(Guid sessionId)
    {
        webCookie.Delete(AuthConstants.SessionCookieName);
        // webCookie.Add(AuthConstants.SessionCookieName, sessionId.ToString());
        
        //TODO: AddSecure
        webCookie.AddSecure(AuthConstants.SessionCookieName, sessionId.ToString());
    }
}