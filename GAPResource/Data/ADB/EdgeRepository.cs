using GAPResource.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;

namespace GAPResource.Data
{
    public class EdgeRepository : BaseRepository<EdgeDTO>
    {
        public EdgeRepository(IDataSource source) : base(source.Edge, source.DB)
        {

        }

        public MainVertex Vertexes(MainVertex fromCity)
        {
            IEnumerable<EdgeDTO> startCity = Items.Where(p => p.CityFrom == fromCity.Vertex || p.CityTo == fromCity.Vertex).ToList();
            var endParentCityIds = startCity.Select(p => new { id = p.GetOtherCity(fromCity.Vertex), price = p.Price }).Distinct().ToList();

            foreach (var endParentCityId in endParentCityIds)
            {
                if (fromCity.UsiallyExists(endParentCityId.id)) continue;
                MainVertex mainVertex = Vertexes(new MainVertex(endParentCityId.id, fromCity, endParentCityId.price));
                fromCity.Childs.Add(mainVertex);
            }

            return fromCity;
        }


        public bool RouteExist(long startCityId, long endCityId, HashSet<long> finishedItems)
        {
            if (startCityId == endCityId) return false;
            IEnumerable<EdgeDTO> startCity = Items.Where(p => p.CityFrom == startCityId || p.CityTo == startCityId).ToList();
            IEnumerable<EdgeDTO> endCity = Items.Where(p => p.CityFrom == endCityId || p.CityTo == endCityId).ToList();
            if (!startCity.Any() || !endCity.Any()) return false;
            var startParentCityId = startCity.Select(p=>p.GetOtherCity(startCityId)).Except(finishedItems).ToList();
            var endParentCityId = endCity.Select(p => p.GetOtherCity(endCityId)).Except(finishedItems).ToList();
            foreach(long newStartCityId in startParentCityId)
            {
                finishedItems.Add(newStartCityId);
                if(newStartCityId == endCityId)
                    return true;
                foreach (long newEndCityId in endParentCityId)
                {
                    if (newStartCityId == newEndCityId || newEndCityId == startCityId) 
                        return true;
                    if(RouteExist(newStartCityId, newEndCityId, finishedItems))
                        return true;
                }
            }
            return false;
        }
    }
}


