using System.ComponentModel.DataAnnotations;

namespace SecretsSharing.ViewModels;

public class FileViewModel
{
    public int? FileId { get; set; }
    
    [Required]
    public string? FileName { get; set; }
    public string? FileContent { get; set; }
    public string? FilePath { get; set; }
    public bool? ShouldBeDeleted { get; set; }
}