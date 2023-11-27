using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GAPResource.Models
{
    public class DetailsViewModel
    {
        public string TruckName { get; set; }
        public long OrderId { get; set; }
        public List<DetailViewModel> Items { get; set; } = new List<DetailViewModel>();
    }
    public class DetailViewModel
    {
        public long OrderId;
        public string? CityTo { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double Weight { get; set; }

    }
}
