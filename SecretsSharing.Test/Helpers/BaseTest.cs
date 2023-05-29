using SecretsSharing.BL.Auth;
using SecretsSharing.BL.General;
using SecretsSharing.BL.Security;
using SecretsSharing.BL.Session;
using SecretsSharing.DAL;
using SecretsSharing.DAL.File;

namespace SecretsSharing.Test.Helpers
{
	public class BaseTest
	{
        protected IAuthDAL authDal = new AuthDAL();
        protected IEncrypt encrypt = new Encrypt();
		protected IAuth authBL;
        protected IDbSessionDAL dbSessionDAL = new DbSessionDAL();
        protected IDbSession dbSession;
        protected IWebCookie webCookie;
        protected IUserTokenDAL userTokenDAL = new UserTokenDAL();
        protected IFileDAL fileDal;

        protected CurrentUser currentUser;

        public BaseTest()
        {
	        fileDal = new FileDAL();
            webCookie = new TestCookie();
            dbSession = new DbSession(dbSessionDAL, webCookie);
            authBL = new Auth(authDal, encrypt, dbSession, userTokenDAL, webCookie);
            currentUser = new CurrentUser(dbSession, webCookie, userTokenDAL, fileDal);
        }
    }
}

