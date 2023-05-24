namespace SecretsSharing.BL.Security;

public interface IEncrypt
{
    string HashPassword(string password, string salt);
}