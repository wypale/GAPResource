using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GAPResource.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        List<OrderViewModel> items;
        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
            items = DataManager.OrderRepository.OrderedItems.Select(p=> new OrderViewModel(p.Id,DataManager.CityRepository.Items.FirstOrDefault(c=>c.Id == p.SenderCityId).Name??"", DataManager.CityRepository.Items.FirstOrDefault(c => c.Id == p.RecipientCityId).Name ?? "", p.Finished)).ToList();
        }

        public IActionResult Index(string message = null)
        {
            ViewBag.Message = message;
            return View(items);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Info(long idOrder)
        {
            return RedirectToAction("Index", "Info", new { idOrder = idOrder });
        }

        public IActionResult DeleteOrder(long idOrder)
        {
            DataManager.OrderRepository.Delete(idOrder);
            return RedirectToAction("Index", new { message = "Заказ успешно удален!" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}