using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DineFine.DataObjects.Models;
using DineFine.Util;
using Microsoft.Extensions.Options;

namespace DineFine.API.Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(IOptions<BlobSettings> blobServiceClient)
    {
        var blobSettingsValue = blobServiceClient.Value;
        _blobServiceClient = new BlobServiceClient(blobSettingsValue.ConnectionString);
    }

    public async Task<FileViewModel> DownloadFileAsync(string uri, CancellationToken cancellationToken)
    {
        var uriBuilder = new BlobUriBuilder(new Uri(uri));
        
        var blobClient = _blobServiceClient.GetBlobContainerClient(uriBuilder.BlobContainerName).GetBlobClient(uriBuilder.BlobName);
        
        var response = await blobClient.DownloadAsync(cancellationToken);
        return new FileViewModel(response.Value.Content, response.Value.ContentType, uriBuilder.BlobName);
    }

    public async Task<string> UploadFileAsync(FileCreateModel fileCreateModel, CancellationToken cancellationToken)
    {
        using var memoryStream = await CopyFileToMemoryStreamAsync(fileCreateModel.File);
        
        NormalizeContainerName(fileCreateModel);
        
        var container = _blobServiceClient.GetBlobContainerClient(fileCreateModel.ContainerName);
        
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        
        var blobClient = container.GetBlobClient(fileCreateModel.BlobName);
        
        await blobClient.UploadAsync(memoryStream, true, cancellationToken);
        
        await blobClient.SetHttpHeadersAsync(
            new BlobHttpHeaders { ContentType = fileCreateModel.File.ContentType }, 
            cancellationToken: cancellationToken);
        
        return blobClient.Uri.AbsoluteUri;
    }
    
    private static async Task<MemoryStream> CopyFileToMemoryStreamAsync(IFormFile file)
    {
        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;
        return memoryStream;
    }
    
    private static void NormalizeContainerName(FileCreateModel model)
    {
        model.ContainerName = model.ContainerName.ToLower().Replace(" ", "-");
    }
}