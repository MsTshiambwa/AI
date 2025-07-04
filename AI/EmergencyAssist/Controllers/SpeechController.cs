using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmergencyAssist.Data;
using EmergencyAssist.Services;
using EmergencyAssist.Models;

public class SpeechController : Controller
{
    private readonly AzureSpeechService _speechService;
    public SpeechController(AzureSpeechService speechService) => _speechService = speechService;

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> Transcribe()
    {
        var text = await _speechService.RecognizeSpeechAsync();
        ViewBag.TranscribedText = text;
        return View("Index");
    }
}
