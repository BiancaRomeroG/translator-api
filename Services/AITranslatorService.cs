using Microsoft.AspNetCore.Components.Forms;
using System.Runtime.CompilerServices;

namespace TranslatorAPI.Services
{
    public class AITranslatorService
    {
        private static readonly string _endpoint = "https://translator-aiservice.cognitiveservices.azure.com/translator/text/batch/v1.0/batches";

        private static readonly string _key = "bfaf7cf49872427fa1892b3dcfc28a6c";

        //private static readonly string _json = ("{\"inputs\": [{\"source\": {\"sourceUrl\": \"https://documentsblob.blob.core.windows.net/source-container?sp=rl&st=2022-09-12T21:24:36Z&se=2022-09-27T05:24:36Z&sv=2021-06-08&sr=c&sig=rlirpQx8OwDCaIniFTU88xe1CgIji1PEMXJeX7KFB1A%3D\",\"storageSource\": \"AzureBlob\",\"language\": \"es\"}, \"targets\": [{\"targetUrl\": \"https://documentsblob.blob.core.windows.net/target-container?sp=wl&st=2022-09-12T21:39:01Z&se=2022-09-27T05:39:01Z&sv=2021-06-08&sr=c&sig=xptoMUu65AM17dz0g%2FiumyaEUiFOeyxLofWopTpxuHM%3D\",\"storageSource\": \"AzureBlob\",\"category\": \"general\",\"language\": \"en\"}]}]}");
    
        private string GenerateJson(string sourceUrl, string sourceToken, string targetUrl, string targetToken, List<string> languages)
        {
            string storageType = $"\"storageType\": \"File\"";
            string source = $"\"source\": {{\"sourceUrl\": \"{sourceUrl}?{sourceToken}\"}}";
            

            foreach (string language in languages)
            {

            }
            return source;
        }
    }
}
