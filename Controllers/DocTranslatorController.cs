using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslatorAPI.Services;

namespace TranslatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocTranslatorController : ControllerBase
    {
        [HttpPost]
        public async Task<Uri> Test(IFormFile document)
        {
            return await BlobContainerService
                .UploadSourceDocument(document, "test");
        }
    }
}
