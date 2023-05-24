using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Required")]
    [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[!@#$%^&*-]).{10,}$",
        ErrorMessage = "Password is too simple")]
    public string? Password { get; set; }
}