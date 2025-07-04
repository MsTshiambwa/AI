using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmergencyAssist.Services;

namespace EmergencyAssist.Controllers
{
    public class PanicController : Controller
    {
        private readonly OpenAIService _openAIService;

        public PanicController(OpenAIService openAIService)
        {
            _openAIService = openAIService;
        }

        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> AskHelp(string message)
        {
            try
            {
                var response = await _openAIService.GetChatResponseAsync(message);
                ViewBag.AIResponse = response;
            }
            catch (Exception ex)
            {
                ViewBag.AIResponse = $"Error: {ex.Message}";
            }

            return View("Index");
        }
    }
}
