using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DineFine.DataObjects.Models;

public record FileViewModel(Stream FileStream, string FileName, string ContentType);

public class FileCreateModel
{
    [Required]
    public IFormFile File { get; set; } = null!;
    
    [Required]
    public string ContainerName { get; set; } = null!;
    
    [Required]
    public string BlobName { get; set; } = null!;
}