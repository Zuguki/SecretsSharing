using System.ComponentModel.DataAnnotations;
using SecretsSharing.DAL.Models;

namespace SecretsSharing.BL.Auth;

public interface IAuth
{
    Task<int> CreateUser(UserModel model);
    Task<int> Authenticate(string email, string password, bool rememberMe);
    Task<ValidationResult?> ValidateEmail(string email);
}