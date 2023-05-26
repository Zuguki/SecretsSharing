namespace SecretsSharing.DAL.Models;

public class FileModel
{
    public int? FileId { get; set; }
    public int UserId { get; set; }
    public string? FileName { get; set; }
    public string? FileContent { get; set; }
    public string? FilePath { get; set; }
}