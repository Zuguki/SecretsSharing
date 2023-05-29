using SecretsSharing.BL.Auth;
using SecretsSharing.DAL.Models;
using SecretsSharing.Test.Helpers;

namespace SecretsSharing.Test
{
    public class CurrentUserTest : BaseTest
    {
        [Test]
        public async Task BaseRegistrationTest()
        {
            using (Helper.CreateTransactionScope())
            {
                await CreateAndAuthUser();

                var isLoggedIn = await currentUser.IsLoggedIn();
                Assert.That(isLoggedIn, Is.True);

                webCookie.Delete(AuthConstants.SessionCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await currentUser.IsLoggedIn();
                Assert.That(isLoggedIn, Is.True);

                webCookie.Delete(AuthConstants.SessionCookieName);
                webCookie.Delete(AuthConstants.RememberMeCookieName);
                dbSession.ResetSessionCache();

                isLoggedIn = await currentUser.IsLoggedIn();
                Assert.That(isLoggedIn, Is.False);
            }
        }

        private async Task CreateAndAuthUser()
        {
            var email = Guid.NewGuid() + "@test.com";

            await authBL.CreateUser(
                new UserModel()
                {
                    Email = email,
                    Password = "Qwerty!234"
                });
            await authBL.Authenticate(email, "Qwerty!234", true);
        }
    }
}