namespace TranslatorAPI.Models
{
    public class TranslatorResponse
    {
        public Uri? SourceUrl { get; set; }
        public Uri? TargetUrl { get; set; }
        public bool Success { get; set; } = true;
    }
}
