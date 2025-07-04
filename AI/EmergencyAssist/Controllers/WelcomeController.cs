using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmergencyAssist.Data;
using EmergencyAssist.Services;
using EmergencyAssist.Models;

public class WelcomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
