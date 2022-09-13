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
        public async Task<string> Test(IList<IFormFile> files)
        {
            return await BlobContainerService
                .UploadSourceDocument(files[0], "test");
        }
    }
}
