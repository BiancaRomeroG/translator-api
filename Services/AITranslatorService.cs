using Microsoft.AspNetCore.Components.Forms;
using System.Runtime.CompilerServices;
using TranslatorAPI.Models;

namespace TranslatorAPI.Services
{
    public abstract class AITranslatorService
    {
        private static readonly string _endpoint = "https://translator-aiservice.cognitiveservices.azure.com/translator/text/batch/v1.0/batches";
        private static readonly string _key = "bfaf7cf49872427fa1892b3dcfc28a6c";

        // Source Container
        static readonly string _sourceName = "source-container";
        static readonly string _sourceToken = "sp=rl&st=2022-09-12T21:24:36Z&se=2022-09-27T05:24:36Z&sv=2021-06-08&sr=c&sig=rlirpQx8OwDCaIniFTU88xe1CgIji1PEMXJeX7KFB1A%3D";
        // Target Container
        static readonly string _targetName = "target-container";
        static readonly string _targetToken = "sp=wl&st=2022-09-12T21:39:01Z&se=2022-09-27T05:39:01Z&sv=2021-06-08&sr=c&sig=xptoMUu65AM17dz0g%2FiumyaEUiFOeyxLofWopTpxuHM%3D";

        public TranslatorResponse TranslateDocument(Uri sourceUrl, string lang)
        {
            string targetUrl = sourceUrl.ToString().Replace(_sourceName, _targetName)
                .Insert(sourceUrl.ToString().LastIndexOf("."), $"-{lang}");

            //string json = generateJson(sourceUrl, _sourceToken, targetUrl, _targetToken, lang);
            return new TranslatorResponse()
            {
                SourceUrl = sourceUrl,
                TargetUrl = new Uri(targetUrl)
            };
        }

        private string GenerateJson(string sourceUrl, string sourceToken, string targetUrl, string targetToken, string lang)
        {
            string storageType = $"\"storageType\": \"File\",";

            string source = $"\"source\": {{\"sourceUrl\": \"{sourceUrl}?{sourceToken}\"}},";

            string newTargetUrl = $"\"targetUrl\": \"{targetUrl}?{targetToken}\",";
            string language = $"\"language\": \"{lang}\"";
            string targets = $"\"targets\": [{{{newTargetUrl},{language}}}]";

            return $"{{\"inputs\": [{{{storageType}{source}{targets}}}]}}";
        }
    }
}
