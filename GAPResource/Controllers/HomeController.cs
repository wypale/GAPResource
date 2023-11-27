using GAPResource.Data;
using GAPResource.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GAPResource.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        List<CityViewModel> senderCity;
        public HomeController(ILogger<HomeController> logger)
        {
            senderCity = DataManager.CityRepository.OrderedItems.Select(p => new CityViewModel(p.Id, p.Name)).ToList();
            _logger = logger;           
        }

        public IActionResult Index(string message)
        {
            ViewBag.SenderCity = new SelectList(senderCity, "Id", "Name");
            ViewBag.Message = message;
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

        [HttpPost]
        public IActionResult Index(SendOrderViewModel model)
        {
            var dates = DataManager.OrderRepository.GetOrdersDate(model.SenderCityId);
            var trucks = DataManager.TruckRepository.Items.OrderBy(p=>p.LoadCapacity).ToList();
            var vertex = DataManager.EdgeRepository.Vertexes(new MainVertex(model.SenderCityId));
            foreach (DateTime date in dates)
            {
                var filterByTheWay = GetByTheWay(date, vertex, model.SenderCityId);
                foreach (var orders in filterByTheWay)
                    SendOrders(orders, trucks, vertex);

            }

            return RedirectToAction("Index", "Home", new { message = "Заказы успешно отправлены!" });
        }

        private List<List<OrderDTO>> GetByTheWay(DateTime date, MainVertex vertex, long senderCityId)
        {
            var orders = DataManager.OrderRepository.GetOrders(date, senderCityId);
            List<List<OrderDTO>> filterByTheWay = new List<List<OrderDTO>>();
            for (int i = 0; i < orders.Count; i++)
            {
                var order1 = orders[i];
                List<OrderDTO> filterByTheWayInner = new List<OrderDTO>();
                for (int j = i; j < orders.Count; j++)
                {
                    var order2 = orders[j];
                    if (vertex.OnTheOneWay(order1.RecipientCityId, order2.RecipientCityId))
                    {
                        filterByTheWayInner.Add(order2);
                        orders.RemoveAt(j);
                        j--;
                    }
                }
                if (filterByTheWayInner.Count > 0)
                {
                    filterByTheWay.Add(filterByTheWayInner.OrderByDescending(p => p.Weight).ToList());
                    i--;
                }
            }
            return filterByTheWay.OrderByDescending(p=>p.Sum(i=>i.Weight)).ToList();
        }

        private void SendOrders(List<OrderDTO> orders,List<TruckDTO> trucks, MainVertex vertex)
        {
            double sumWeight = orders.Sum(i => i.Weight);
            TruckDTO truck;
            if ((truck = trucks.FirstOrDefault(p=>p.LoadCapacity >= sumWeight))!=null)
            {
                SaveOrders(orders, truck, vertex);
            }
            else
            {
                while ( orders.Count > 0 )
                {
                    List<OrderDTO> tempOrders = new List<OrderDTO>();
                    truck = null;
                    foreach(OrderDTO dto in orders)
                    {
                        sumWeight = tempOrders.Sum(p => p.Weight) + dto.Weight;
                        TruckDTO tempTruck;
                        if ((tempTruck = trucks.FirstOrDefault(p => p.LoadCapacity >= sumWeight)) != null)
                        {
                            truck = tempTruck;
                            tempOrders.Add(dto);
                        }
                        else
                        {
                            break;
                        }                      
                    }
                    if(tempOrders.Count == 0)
                    {
                        break;
                    }
                    SaveOrders(tempOrders, truck, vertex);
                    foreach (OrderDTO dto in tempOrders)
                    {
                        orders.Remove(dto);
                    }
                    trucks.Remove(truck);
                }
            }
        }

        private void SaveOrders(List<OrderDTO> orders, TruckDTO truck, MainVertex vertex)
        {
            var ordersWithLevel = vertex.GetPriceByWayAndPart(orders);
            int lastLevel = 0;
            orders = new List<OrderDTO>();
            while (ordersWithLevel.Count > 0 && lastLevel < ordersWithLevel.Max(p => p.Vertex.Level))
            {
                var orderWithLevel = ordersWithLevel.FirstOrDefault(p => p.Vertex.Level > lastLevel);
                double amount = orderWithLevel.Vertex.GetAmount(lastLevel);
                long sumCount = ordersWithLevel.Where(p => p.Vertex.Level >= lastLevel).Count(); // Sum(p => p.Order.Weight);
                ordersWithLevel = ordersWithLevel.Select(p =>
                {
                    p.Order.Amount += amount / sumCount;
                    p.Order.TruckId = truck.Id;
                    p.Order.Finished = true;
                    return p;
                }).ToList();
                lastLevel = orderWithLevel.Vertex.Level;
                orders.AddRange(ordersWithLevel.Where(p => p.Vertex.Level <= lastLevel).Select(p => p.Order));
                ordersWithLevel = ordersWithLevel.Where(p => p.Vertex.Level > lastLevel).ToList();
            }
            DataManager.OrderRepository.UpdateRange(orders);
        }



    }
}