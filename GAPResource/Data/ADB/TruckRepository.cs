using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace GAPResource.Data
{
    public class TruckRepository : BaseIdRepository<TruckDTO>
    {
        public TruckRepository(IDataSource source) : base(source.Truck, source.DB)
        {
            //FormattableString q = $"Select name from City where id=1";
            //var studentName = DB.Database.SqlQueryRaw<string>("Select name from City where id={0}", 1).ToList().FirstOrDefault();
        }

        public double GetLoadCapacity()
        {
            return Items.Sum(p=>p.LoadCapacity);
        }
    }

}
