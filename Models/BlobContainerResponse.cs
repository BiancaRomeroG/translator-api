namespace TranslatorAPI.Models
{
    public class BlobContainerResponse
    {
        public Uri? DocumentUrl { get; set; }
        public bool Success { get; set; } = true;
    }
}
