using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Configuration;

namespace EmergencyAssist.Services
{
    public class AzureFaceApiService
    {
        private readonly IFaceClient _faceClient;

        public AzureFaceApiService(IConfiguration config)
        {
            _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(config["Azure:FaceApiKey"]))
            {
                Endpoint = config["Azure:FaceEndpoint"]
            };
        }

        public async Task<IList<DetectedFace>> DetectEmotionsAsync(Stream imageStream)
        {
            var emotions = await _faceClient.Face.DetectWithStreamAsync(imageStream,
                returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.Emotion });
            return emotions;
        }
    }
}