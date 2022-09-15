using Newtonsoft.Json;
using System.Text;
using TranslatorAPI.Models;
using TranslatorAPI.Shared;

namespace TranslatorAPI.Services
{
    public static class AITranslatorService
    {

        public static async Task<TranslatorResponse> TranslateDocumentAsync(Uri sourceUrl, string lang)
        {
            string targetUrl = sourceUrl.ToString().Replace(Constants.SOURCE_CONTAINERNAME, Constants.TARGET_CONTAINERNAME)
                .Insert(sourceUrl.ToString().LastIndexOf("."), $"-{lang}");

            // Delete if exists
            string documentName = targetUrl.Substring(targetUrl.IndexOf(Constants.TARGET_CONTAINERNAME))
                        .Replace($"{Constants.TARGET_CONTAINERNAME}/", "");
            await BlobContainerService.DeleteDocument(Constants.TARGET_CONTAINERNAME, documentName);

            string json = GenerateJson(sourceUrl.ToString(), targetUrl, lang);
            
            using HttpClient client = new();
            using HttpRequestMessage request = new();
            {
                StringContent content = new(json, Encoding.UTF8, "application/json");

                request.Method = HttpMethod.Post;
                request.RequestUri = new Uri(Constants.AITRANSLATOR_ENDPOINT);
                request.Headers.Add("Ocp-Apim-Subscription-Key", Constants.AITRANSLATOR_KEY);
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

            string source = $"\"source\": {{\"sourceUrl\": \"{sourceUrl}?{Constants.SOURCE_CONTAINERTOKEN}\"}},";

            string newTargetUrl = $"\"targetUrl\": \"{targetUrl}?{Constants.TARGET_CONTAINERTOKEN}\",";
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
                request.Headers.Add("Ocp-Apim-Subscription-Key", Constants.AITRANSLATOR_KEY);

                HttpResponseMessage response = await client.SendAsync(request);
                string result = response.Content.ReadAsStringAsync().Result;

                var resultObj  = JsonConvert.DeserializeObject<dynamic>(result);

                Console.WriteLine(resultObj!.status);

                return resultObj!.status == "Succeeded";
            }
        }
    }
}
