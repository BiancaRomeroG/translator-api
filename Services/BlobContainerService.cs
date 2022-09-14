using Azure.Storage.Blobs;
using TranslatorAPI.Models;

namespace TranslatorAPI.Services
{
    public abstract class BlobContainerService
    {
        static readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=documentsblob;AccountKey=AQTIyu84QoMX5YNP+lecZXdRaIu+DzseH0ZouXSR79KNmstzvsSvCbOtA48yTR/k4B20MHgH46Ep+AStUH4qDQ==;EndpointSuffix=core.windows.net";
        
        // Source Container
        static readonly string _sourceName = "source-container";
        // Target Container
        static readonly string _targetName = "target-container";

        public static async Task<BlobContainerResponse> UploadSourceDocument(IFormFile document, string userId) { 
            BlobContainerClient blobContainerClient = 
                new(_connectionString, _sourceName);

            using var stream = new MemoryStream();
            await document.CopyToAsync(stream);
            stream.Position = 0;

            BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}/{document.FileName}");
            await blobClient.UploadAsync(stream, overwrite: true);

            return new BlobContainerResponse()
            {
                DocumentUrl = blobClient.Uri,
            };
        }

        public static async Task<bool> DeleteDocument(string containerName, string documentName)
        {
            BlobContainerClient blobContainerClient =
                new(_connectionString, containerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(documentName);
            return await blobClient.DeleteIfExistsAsync();
        }

    }
}
