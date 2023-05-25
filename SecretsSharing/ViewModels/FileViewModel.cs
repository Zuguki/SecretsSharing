using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.ViewModels;

public class FileViewModel
{
    [Required]
    public string? FileName { get; set; }
    
    [Required]
    public string? FileContent { get; set; }
}