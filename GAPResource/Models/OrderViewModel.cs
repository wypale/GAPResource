using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace GAPResource.Models
{
    public record class OrderViewModel(long Id, string? CityFrom, string? CityTo, bool Finished);
    public class OrderFullViewModel
    {
        public long Id;
        public string? CityFrom { get; set; }
        public string? CityTo { get; set; }
        public string RecipientAddress { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double Weight { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:#.##}")]
        public double? Amount { get; set; }

        public long? TruckId { get; set; }
}
}
