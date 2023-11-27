using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GAPResource.Data
{
    public class CityRepository : BaseIdRepository<CityDTO>
    {
        public CityRepository(IDataSource source) : base(source.City, source.DB)
        {

        }
    }
}

