using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Runtime.CompilerServices;
using System.Text;
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

        public static async Task<TranslatorResponse> TranslateDocumentAsync(Uri sourceUrl, string lang)
        {
            string targetUrl = sourceUrl.ToString().Replace(_sourceName, _targetName)
                .Insert(sourceUrl.ToString().LastIndexOf("."), $"-{lang}");

            // Delete if exists
            string documentName = targetUrl.Substring(targetUrl.IndexOf(_targetName))
                        .Replace($"{_targetName}/", "");
            await BlobContainerService.DeleteDocument(_targetName, documentName);

            string json = GenerateJson(sourceUrl.ToString(), targetUrl, lang);
            
            using HttpClient client = new();
            using HttpRequestMessage request = new();
            {
                StringContent content = new(json, Encoding.UTF8, "application/json");

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(_endpoint);
                request.Headers.Add("Ocp-Apim-Subscription-Key", _key);
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    return new TranslatorResponse()
                    {
                        Success = false
                    };

                }

                string operationLocation = response.Headers.GetValues("Operation-Location").FirstOrDefault()!;

                bool finished = false;
                while (!finished)
                {
                    finished = await IsTranslated(operationLocation);
                    await Task.Delay(2000);
                }
            }

            return new TranslatorResponse()
            {
                SourceUrl = sourceUrl,
                TargetUrl = new Uri(targetUrl),
            };
        }

        private static string GenerateJson(string sourceUrl, string targetUrl, string lang)
        {
            string storageType = $"\"storageType\": \"File\",";

            string source = $"\"source\": {{\"sourceUrl\": \"{sourceUrl}?{_sourceToken}\"}},";

            string newTargetUrl = $"\"targetUrl\": \"{targetUrl}?{_targetToken}\",";
            string language = $"\"language\": \"{lang}\"";
            string targets = $"\"targets\": [{{{newTargetUrl}{language}}}]";

            return $"{{\"inputs\": [{{{storageType}{source}{targets}}}]}}";
        }

        private static async Task<bool> IsTranslated(string endpoint) 
        {
            HttpClient client = new HttpClient();
            using HttpRequestMessage request = new HttpRequestMessage();
            {
                request.Method = HttpMethod.Get;
                request.RequestUri = new Uri(endpoint);
                request.Headers.Add("Ocp-Apim-Subscription-Key", _key);

                HttpResponseMessage response = await client.SendAsync(request);
                string result = response.Content.ReadAsStringAsync().Result;

                var resultObj  = JsonConvert.DeserializeObject<dynamic>(result);

                Console.WriteLine(resultObj!.status);

                return resultObj!.status == "Succeeded";
            }
        }
    }
}
