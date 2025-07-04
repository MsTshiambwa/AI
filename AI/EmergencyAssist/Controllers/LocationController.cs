using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EmergencyAssist.Data;
using EmergencyAssist.Services;


public class LocationController : Controller
{
    private readonly AzureMapsService _mapsService;
    public LocationController(AzureMapsService mapsService) => _mapsService = mapsService;

    public IActionResult Index() => View();

    [HttpPost]
    public async Task<IActionResult> GetAddress(double latitude, double longitude)
    {
        var address = await _mapsService.ReverseGeocodeAsync(latitude, longitude);
        ViewBag.Address = address;
        return View("Index");
    }
}
