using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TimeChecker.Models;

/**
 * Die Klasse HomeController ist die Schnittstelle zwischen 
 * Datenbank und den HTMl Seiten (View's) im Ordner "Views" -> "Home" und "Shared" angelegt.
 * 
 * Methoden: 
 * 
 * Index() - Die Hauptseite von Home View.
 * 
 * Privacy() - Aufruf von Privacy View.   
 * 
 * Error() - Aufruf von Error View falls keine Request ID gefunden wurde.
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */

namespace TimeChecker.Controllers
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
