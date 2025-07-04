using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;

namespace EmergencyAssist.Services
{
    public class AzureSpeechService
    {
        private readonly string _key;
        private readonly string _region;

        public AzureSpeechService(IConfiguration config)
        {
            _key = config["Azure:SpeechKey"];
            _region = config["Azure:SpeechRegion"];
        }

        public async Task<string> RecognizeSpeechAsync()
        {
            var config = SpeechConfig.FromSubscription(_key, _region);
            using var recognizer = new SpeechRecognizer(config);
            var result = await recognizer.RecognizeOnceAsync();
            return result.Text;
        }
    }
}