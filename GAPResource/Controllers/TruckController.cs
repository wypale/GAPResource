using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GAPResource.Controllers
{
    public class TruckController : Controller
    {
        private readonly ILogger<CityController> _logger;
        List<TruckDTO> items;
        public TruckController(ILogger<CityController> logger)
        {
            _logger = logger;
            items = DataManager.TruckRepository.OrderedItems.ToList();
        }

        public IActionResult Index()
        {
            return View(items);
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