using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslatorAPI.Models;
using TranslatorAPI.Services;

namespace TranslatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocTranslatorController : ControllerBase
    {
        [HttpPost]
        public async Task<TranslatorResponse> TranslateDocument(IFormFile document, string userId, string lang)
        {
            if (document == null || string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(lang))
            {
                return new TranslatorResponse()
                {
                    Success = false,
                };
            }
            var sourceBlob = await BlobContainerService.UploadSourceDocument(document, userId);

            return await AITranslatorService.TranslateDocumentAsync(sourceBlob.DocumentUrl!, lang);
        }

        [HttpGet]
        public Task<List<BlobResponse>> GetTranslatedDocuments(string userId)
        {
            return Task.FromResult(BlobContainerService.GetTargetDocuments(userId));
        }
    }
}
