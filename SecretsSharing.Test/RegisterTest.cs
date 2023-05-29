using SecretsSharing.BL.Exceptions;
using SecretsSharing.DAL.Models;
using SecretsSharing.Test.Helpers;

namespace SecretsSharing.Test;

public class RegisterTests : BaseTest
{
    [Test]
    public async Task BaseRegistrationTest()
    {
        using (Helper.CreateTransactionScope())
        {
            var email = Guid.NewGuid() + "@test.com";

            authBL.ValidateEmail(email).GetAwaiter().GetResult();

            var userId = await authBL.CreateUser(
                new UserModel
                {
                    Email = email,
                    Password = "Qwerty!234"
                });

            Assert.That(userId, Is.GreaterThan(0));

            var userDalResult = await authDal.GetUser(userId);
            Assert.Multiple(() =>
            {
                Assert.That(email, Is.EqualTo(userDalResult.Email));
                Assert.That(userDalResult.Salt, Is.Not.Null);
            });
            var userByEmailDalResult = await authDal.GetUser(email);
            Assert.That(email, Is.EqualTo(userByEmailDalResult.Email));

            Assert.Throws<DuplicateEmailException>(delegate { authBL.ValidateEmail(email).GetAwaiter().GetResult(); });

            var encryptPassword = encrypt.HashPassword("Qwerty!234", userByEmailDalResult.Salt);
            Assert.That(encryptPassword, Is.EqualTo(userByEmailDalResult.Password));
        }
    }
}