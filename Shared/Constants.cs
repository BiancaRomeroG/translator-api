namespace TranslatorAPI.Shared
{
    public static class Constants
    {
        // Documents Storage
        public const string STORAGEACOUNT_CONNSTRING = "DefaultEndpointsProtocol=https;AccountName=documentsstorage;AccountKey=z+w1MIAolQ3tGC88dUbZGbyyORrKpcdQeg+dCRtMErIZGZ2QphsbMANEe342htjcIUP8dy2hHxzW+AStDYOIdA==;EndpointSuffix=core.windows.net";

        // Source
        public const string SOURCE_CONTAINERNAME = "source-container";
        public const string SOURCE_CONTAINERTOKEN = "sp=racwdli&st=2022-12-31T02:39:06Z&se=2023-11-27T10:39:06Z&spr=https&sv=2021-06-08&sr=c&sig=Q6oaSYCC%2FD1n86pq44%2FbdHxEcewQ4Hm6ay5zvZLKXHs%3D";

        // Target
        public const string TARGET_CONTAINERNAME = "target-container";
        public const string TARGET_CONTAINERTOKEN = "sp=wl&st=2022-12-31T02:47:57Z&se=2022-12-31T10:47:57Z&spr=https&sv=2021-06-08&sr=c&sig=sIpeP0G%2F9m8OxbRXINLIflk2zPEhTF%2Foj9Tg7LokZTk%3D";

        // AITranslator
        public const string AITRANSLATOR_ENDPOINT = "https://ai-translator-service.cognitiveservices.azure.com/translator/text/batch/v1.0/batches";
        public const string AITRANSLATOR_KEY = "168b2f8c46844a48b38f794cd18253de";


    }
}
