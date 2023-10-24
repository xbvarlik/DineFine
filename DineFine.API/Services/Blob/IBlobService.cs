using DineFine.DataObjects.Models;

namespace DineFine.API.Services;

public interface IBlobService
{
    Task<FileViewModel> DownloadFileAsync(string uri , CancellationToken cancellationToken);
    Task<string> UploadFileAsync(FileCreateModel fileCreateModel, CancellationToken cancellationToken);
}