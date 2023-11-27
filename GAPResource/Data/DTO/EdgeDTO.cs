using System.ComponentModel.DataAnnotations.Schema;

namespace GAPResource.Data
{
    public class EdgeDTO : DTO
    {
        [Column("CityFirstId")]
        public long CityFrom { get; set; }
        [Column("CitySecondId")]
        public long CityTo { get; set; }
        public double Price { get; set; }

        public long GetOtherCity(long id)
        {
            if (CityFrom != id) return CityFrom;
            return CityTo;
        }
    }
}
