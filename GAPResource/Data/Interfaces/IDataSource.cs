using GAPResource.Data;
using Microsoft.EntityFrameworkCore;

namespace GAPResource.Interfaces
{
    public interface IDataSource
    {
        public DbSet<CityDTO> City { get; set; }
        public DbSet<TruckDTO> Truck { get; set; }
        public DbSet<EdgeDTO> Edge { get; set; }
        public DbSet<OrderDTO> Order { get; set; }
        public DbContext DB { get; set; }
    }
}
