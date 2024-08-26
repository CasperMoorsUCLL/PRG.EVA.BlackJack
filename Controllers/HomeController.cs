using Microsoft.AspNetCore.Mvc;
using PRG.EVA.BlackJack.Models;
using System.Diagnostics;

namespace PRG.EVA.BlackJack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Play()
        {
            return View();
        }
        public IActionResult Won()
        {
            return View();
        }
        public IActionResult Lost()
        {
            return View();
        }
        public IActionResult Draw()
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
