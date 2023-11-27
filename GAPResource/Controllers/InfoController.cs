using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GAPResource.Controllers
{
    public class InfoController : Controller
    {
        private readonly ILogger<InfoController> _logger;
        OrderFullViewModel item;
        public InfoController(ILogger<InfoController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(long idOrder, string message = null)
        {
            ViewBag.Message = message;
            var order = DataManager.OrderRepository.Items.FirstOrDefault(p => p.Id == idOrder);
            var cityFrom = DataManager.CityRepository.Items.FirstOrDefault(c => c.Id == order.SenderCityId);
            var cityTo = DataManager.CityRepository.Items.FirstOrDefault(c => c.Id == order.RecipientCityId);
            item = new OrderFullViewModel() { CityFrom =  cityFrom.Name ?? "", CityTo = cityTo.Name ?? "", RecipientAddress= order.RecipientAddress, Weight= order.Weight, Date=order.Date, Amount = order.Amount, TruckId = order.TruckId, Id = idOrder };
            return View(item);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(long idOrder)
        {
            DetailsViewModel model = new DetailsViewModel();
            var orderMain = DataManager.OrderRepository.Items.FirstOrDefault(p => p.Id == idOrder);
            model.OrderId = orderMain.Id;
            model.TruckName = DataManager.TruckRepository.Items.FirstOrDefault(p => p.Id == orderMain.TruckId)?.Name;
            foreach(var order in DataManager.OrderRepository.Items.Where(p=>p.Date == orderMain.Date && p.TruckId == orderMain.TruckId && p.Finished))
            {
                model.Items.Add(new DetailViewModel() { CityTo = DataManager.CityRepository.Items.FirstOrDefault(p=>p.Id == order.RecipientCityId)?.Name, Weight = order.Weight, OrderId = order.Id });
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}