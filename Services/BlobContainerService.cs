using Azure.Storage.Blobs;
using TranslatorAPI.Models;
using TranslatorAPI.Shared;

namespace TranslatorAPI.Services
{
    public static class BlobContainerService
    {
        public static async Task<BlobResponse> UploadSourceDocument(IFormFile document, string userId)
        {
            BlobContainerClient blobContainerClient =
                new(Constants.STORAGEACOUNT_CONNSTRING, Constants.SOURCE_CONTAINERNAME);

            using var stream = new MemoryStream();
            await document.CopyToAsync(stream);
            stream.Position = 0;

            BlobClient blobClient = blobContainerClient.GetBlobClient($"{userId}/{document.FileName}");
            await blobClient.UploadAsync(stream, overwrite: true);

            return new BlobResponse()
            {
                DocumentUrl = blobClient.Uri,
            };
        }

        public static async Task<bool> DeleteDocument(string containerName, string documentName)
        {
            BlobContainerClient blobContainerClient =
                new(Constants.STORAGEACOUNT_CONNSTRING, containerName);

            BlobClient blobClient = blobContainerClient.GetBlobClient(documentName);
            return await blobClient.DeleteIfExistsAsync();
        }

        public static List<BlobResponse> GetTargetDocuments(string userId)
        {
            BlobContainerClient blobContainerClient =
                new(Constants.STORAGEACOUNT_CONNSTRING, Constants.TARGET_CONTAINERNAME);

            List<string> blobNames = blobContainerClient.GetBlobs(prefix: userId).Select(b => b.Name).ToList();

            List<BlobResponse> documents = new();
            foreach (string blobName in blobNames)
            {
                documents.Add(new BlobResponse()
                {
                    DocumentUrl = new Uri($"{blobContainerClient.Uri}/{blobName}")
                });
            }

            return documents;
        }

    }
}
