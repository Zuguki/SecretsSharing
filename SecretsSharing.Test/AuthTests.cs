using SecretsSharing.BL.Auth;
using SecretsSharing.BL.Exceptions;
using SecretsSharing.DAL.Models;
using SecretsSharing.Test.Helpers;

namespace SecretsSharing.Test
{
	public class AuthTest : BaseTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task AuthRegistrationTest()
        {
            using (Helper.CreateTransactionScope())
            {
                var email = Guid.NewGuid() + "@test.com";

                await authBL.CreateUser(
                    new UserModel
                    {
                        Email = email,
                        Password = "Qwerty!234"
                    });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("sefrew", "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate(email, "111", false).GetAwaiter().GetResult(); });

                Assert.Throws<AuthorizationException>(delegate { authBL.Authenticate("werewr", "qwer1234", false).GetAwaiter().GetResult(); });

                await authBL.Authenticate(email, "Qwerty!234", false);

                var authCookie = webCookie.Get(AuthConstants.SessionCookieName);
                Assert.That(authCookie, Is.Not.Null);

                var rememberMeCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.That(rememberMeCookie, Is.Null);
            }
        }

        [Test]
        public async Task RememberMeTest()
        {
            using (Helper.CreateTransactionScope())
            {
                var email = Guid.NewGuid() + "@test.com";

                var userId = await authBL.CreateUser(
                    new UserModel()
                    {
                        Email = email,
                        Password = "Qwerty!234"
                    });

                await authBL.Authenticate(email, "Qwerty!234", true);

                var authCookie = webCookie.Get(AuthConstants.SessionCookieName);
                Assert.That(authCookie, Is.Not.Null);

                var rememberMeCookie = webCookie.Get(AuthConstants.RememberMeCookieName);
                Assert.That(rememberMeCookie, Is.Not.Null);
            }
        }
    }
}
