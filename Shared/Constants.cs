namespace TranslatorAPI.Shared
{
    public static class Constants
    {
        // Documents Storage
        public const string STORAGEACOUNT_CONNSTRING = "DefaultEndpointsProtocol=https;AccountName=documentsstorage;AccountKey=z+w1MIAolQ3tGC88dUbZGbyyORrKpcdQeg+dCRtMErIZGZ2QphsbMANEe342htjcIUP8dy2hHxzW+AStDYOIdA==;EndpointSuffix=core.windows.net";

        // Source
        public const string SOURCE_CONTAINERNAME = "source-container";
        public const string SOURCE_CONTAINERTOKEN = "sp=rl&st=2022-09-15T18:06:21Z&se=2022-12-22T02:06:21Z&sv=2021-06-08&sr=c&sig=VRAbIDw3Sma4TTqjj3xAaTb4heXQmreI8LHoWJbCuKs%3D";

        // Target
        public const string TARGET_CONTAINERNAME = "target-container";
        public const string TARGET_CONTAINERTOKEN = "sp=wl&st=2022-09-15T18:08:06Z&se=2022-12-22T02:08:06Z&sv=2021-06-08&sr=c&sig=JXwNWJPVtmFdWhxTwu1or5jNv%2FXC%2BAcIRsajRmUnzuo%3D";

        // AITranslator
        public const string AITRANSLATOR_ENDPOINT = "https://ai-translator-service.cognitiveservices.azure.com/translator/text/batch/v1.0/batches";
        public const string AITRANSLATOR_KEY = "168b2f8c46844a48b38f794cd18253de";


    }
}
