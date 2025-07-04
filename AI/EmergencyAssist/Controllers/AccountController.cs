using Microsoft.AspNetCore.Mvc;
using EmergencyAssist.Models;
using EmergencyAssist.Data;
using Microsoft.AspNetCore.Http;


using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;
    public AccountController(ApplicationDbContext context) => _context = context;

    public IActionResult Login() => View();
    public IActionResult Register() => View();

    [HttpPost]
    [HttpPost]
    public IActionResult Login(User model)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
        if (user != null)
        {
            // ✅ Store username in session
            HttpContext.Session.SetString("Username", user.Username);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid credentials";
        return View();
    }


    [HttpPost]
    public IActionResult Register(User model)
    {
        _context.Users.Add(model);
        _context.SaveChanges();
        return RedirectToAction("Login");

         // ✅ Optionally set session and redirect to home
        HttpContext.Session.SetString("Username", model.Username);
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Clear session on logout
        return RedirectToAction("Index", "Welcome"); // Redirect to Welcome page (or Login page)
    }
}

