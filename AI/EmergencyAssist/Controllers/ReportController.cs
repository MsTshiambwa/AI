using Microsoft.AspNetCore.Mvc;
using EmergencyAssist.Data;
using EmergencyAssist.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace EmergencyAssist.Controllers
{
    public class ReportController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reports = _context.Reports.OrderByDescending(r => r.Timestamp).ToList();
            return View(reports);
        }

        [HttpPost]
        public IActionResult Submit(string message)
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            var report = new Report
            {
                Message = message,
                SubmittedBy = username,
                Timestamp = DateTime.UtcNow // Corrected field name
            };

            _context.Reports.Add(report);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}
