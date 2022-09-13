using Azure.Storage.Blobs;

namespace TranslatorAPI.Services
{
    public abstract class BlobContainerService
    {
        private static readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=documentsblob;AccountKey=AQTIyu84QoMX5YNP+lecZXdRaIu+DzseH0ZouXSR79KNmstzvsSvCbOtA48yTR/k4B20MHgH46Ep+AStUH4qDQ==;EndpointSuffix=core.windows.net";
        
        // Source Container
        private static readonly string _sourceName = "source-container";
        static readonly string sourceToken = "sp=rl&st=2022-09-12T21:24:36Z&se=2022-09-27T05:24:36Z&sv=2021-06-08&sr=c&sig=rlirpQx8OwDCaIniFTU88xe1CgIji1PEMXJeX7KFB1A%3D";

        // Target Container
        private static readonly string _targetName = "target-container";
        static readonly string targetToken = "sp=wl&st=2022-09-12T21:39:01Z&se=2022-09-27T05:39:01Z&sv=2021-06-08&sr=c&sig=xptoMUu65AM17dz0g%2FiumyaEUiFOeyxLofWopTpxuHM%3D";

        public static async Task<Uri> UploadSourceDocument(IFormFile document, string userId) { 
            BlobContainerClient blobContainerClient = 
                new BlobContainerClient(_connectionString, _sourceName);

            Uri documentUri;

            using(var stream = new MemoryStream())
            {
                await document.CopyToAsync(stream);
                stream.Position = 0;

                BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}/{document.FileName}");
                await blobClient.UploadAsync(stream, overwrite: true);

                documentUri = blobClient.Uri;
            }

            return documentUri;
        }

    }
}
