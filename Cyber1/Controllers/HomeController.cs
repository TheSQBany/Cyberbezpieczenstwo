using Cyber1.Areas.Identity.Data;
using Cyber1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Cyber1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Logs()
        {
            var logs = _context.Logs;
            return View(logs);

        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult HomeBtn()
        {
            var newlog = new LoggModel()
            {
                UserId = User.Identity.Name, //set the value, or set this column as auto increase (Identity).
                ActivityDate = DateTime.Now,
                Action = "Logowanie",
                Description = "Pomyślnie zalogowano"
            };

            _context.Logs.Add(newlog); //insert into the database 
            _context.SaveChanges();  //save change.

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}