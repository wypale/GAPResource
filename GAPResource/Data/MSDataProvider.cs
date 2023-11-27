using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GAPResource.Data
{
    public class MSDataProvider
    {
        public CityRepository CityRepository { get; }
        public TruckRepository TruckRepository { get; }

        public EdgeRepository EdgeRepository { get; }

        public OrderRepository OrderRepository { get; }

        public MSDataProvider(IDataSource source)
        {
            CityRepository = new CityRepository(source);
            TruckRepository = new TruckRepository(source);
            EdgeRepository = new EdgeRepository(source);
            OrderRepository = new OrderRepository(source);
        }
    }
}
