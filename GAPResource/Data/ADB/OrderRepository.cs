using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GAPResource.Data
{
    public class OrderRepository : BaseIdRepository<OrderDTO>
    {
        public OrderRepository(IDataSource source) : base(source.Order, source.DB)
        {

        }

        public List<DateTime> GetOrdersDate(long cityFromId)
        {
            return Items.Where(p => !p.Finished && cityFromId == p.SenderCityId).Select(p => p.Date).Distinct().ToList();
        }


        public List<OrderDTO> GetOrders(DateTime date, long cityFromId)
        {
            return Items.Where(p => p.Date == date && p.Date >= DateTime.Now.Date && !p.Finished && cityFromId == p.SenderCityId).ToList();
        }

        public double GetLoadCapacity(DateTime date, long cityFrom)
        {
            return Items.Where(p => p.Date == date && !p.Finished && cityFrom == p.SenderCityId).Sum(p => p.Weight);
        }
    }
}