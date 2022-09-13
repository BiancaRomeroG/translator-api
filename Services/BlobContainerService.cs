using Azure.Storage.Blobs;

namespace TranslatorAPI.Services
{
    public abstract class BlobContainerService
    {
        private static string _connectionString = "DefaultEndpointsProtocol=https;AccountName=documentsblob;AccountKey=AQTIyu84QoMX5YNP+lecZXdRaIu+DzseH0ZouXSR79KNmstzvsSvCbOtA48yTR/k4B20MHgH46Ep+AStUH4qDQ==;EndpointSuffix=core.windows.net";
        
        // Source Container
        private static string _sourceName = "source-container";

        // Target Container
        private static string _targetName = "target-container";

        public static async Task<string> UploadSourceDocument(IFormFile document, string userId) { 
            BlobContainerClient blobContainerClient = 
                new BlobContainerClient(_connectionString, _sourceName);

            using(var stream = new MemoryStream())
            {
                await document.CopyToAsync(stream);
                stream.Position = 0;
                await blobContainerClient
                    .UploadBlobAsync($"{userId}/{document.FileName}", stream);
            }

            var documentUri = $"{blobContainerClient.Uri}/{document.FileName}";
            return documentUri;
        }

    }
}
