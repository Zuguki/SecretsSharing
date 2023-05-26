using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.ViewModels;

public class TextViewModel
{
    [Required]
    public string? Title { get; set; }
    
    [Required]
    public string? Text { get; set; }
}