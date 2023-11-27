using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GAPResource.Controllers
{
    public class CityController : Controller
    {
        private readonly ILogger<CityController> _logger;
        List<CityViewModel> senderCity;
        public CityController(ILogger<CityController> logger)
        {
            _logger = logger;
            senderCity = DataManager.CityRepository.OrderedItems.Select(p => new CityViewModel(p.Id, p.Name)).ToList();
        }

        public IActionResult Index()
        {
            IndexSenderViewModel viewModel = new() { SenderCity = senderCity };
            return View(viewModel);
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