using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using EmergencyAssist.Data;
using EmergencyAssist.Services;
using System.IO;
using System.Linq;

public class EmotionController : Controller
{
    private readonly AzureFaceApiService _faceApiService;
    public EmotionController(AzureFaceApiService faceApiService) => _faceApiService = faceApiService;

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Detect(IFormFile image)
    {
        if (image == null || image.Length == 0) return View("Index");

        using var stream = image.OpenReadStream();
        var faces = await _faceApiService.DetectEmotionsAsync(stream);

        if (faces.Count > 0)
        {
            var emotions = faces[0].FaceAttributes.Emotion;
            var topEmotion = emotions.GetType().GetProperties()
                .Select(p => new { Name = p.Name, Value = (double)p.GetValue(emotions) })
                .OrderByDescending(e => e.Value)
                .First();

            ViewBag.Emotion = $"{topEmotion.Name} ({topEmotion.Value:P0})";
        }

        return View("Index");
    }
}
