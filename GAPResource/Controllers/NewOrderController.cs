using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace GAPResource.Controllers
{
    public class NewOrderController : Controller
    {
        private readonly ILogger<NewOrderController> _logger;
        List<CityViewModel> senderCity;
        List<CityViewModel> recipientCity;
        public NewOrderController(ILogger<NewOrderController> logger)
        {
            _logger = logger;
            senderCity = DataManager.CityRepository.OrderedItems.Select(p => new CityViewModel(p.Id, p.Name)).ToList();
            recipientCity = senderCity;
        }

        public IActionResult Index()
        {
            ViewBag.SenderCity = new SelectList(senderCity, "Id", "Name");
            ViewBag.RecipientCity = new SelectList(recipientCity, "Id", "Name");
            return View();
        }

        //[HttpPost]
        //public IActionResult Index(long senderCityId, string senderAddress, long recipientCityId, string recipientAddress, decimal idWeight, DateTime idDate)
        //{
        //   // var dsfdsf = Request.Form["idDate"];

        //    var dsfdsf2 = RouteData.Values["idDate"];

        //    var dsfdsf3 = Request.Query["idDate"];
        //    IndexSenderRecipientViewModel viewModel = new() { SenderCity = senderCity, RecipientCity = recipientCity };


        //    return View();
        //}


        [HttpPost]
        public IActionResult Index(CreateOrderViewModel form)
        {
            ViewBag.SenderCity = new SelectList(senderCity, "Id", "Name");
            ViewBag.RecipientCity = new SelectList(recipientCity, "Id", "Name");
            if (!DataManager.EdgeRepository.RouteExist(form.RecipientCityId, form.SenderCityId, new HashSet<long>()))
            {
                ViewBag.Message = "Не найден маршрут!";
                return View();
            }

            if (DataManager.OrderRepository.GetLoadCapacity(form.IdDate, form.SenderCityId) + form.IdWeight.GetDouble() > DataManager.TruckRepository.GetLoadCapacity())
            {
                ViewBag.Message = "Превышена грузоподъемность на дату отправки из выбранного города!";
                return View();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            DataManager.OrderRepository.Add(new OrderDTO() { Date = form.IdDate, CreateDate = DateTime.Now, RecipientAddress = form.RecipientAddress,
             Weight = form.IdWeight.GetDouble(), RecipientCityId = form.RecipientCityId, SenderAddress = form.SenderAddress, SenderCityId = form.SenderCityId});

            ViewBag.Message = "Заказ успешно создан!";
            ModelState.Clear();
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