using System.ComponentModel.DataAnnotations;

namespace GAPResource.Data
{
    public class OrderDTO : IdDTO
    {
        public string? SenderAddress { get; set; }
        public string? RecipientAddress { get; set; }
        public double Weight { get; set; }

        public DateTime Date { get; set; }

        public DateTime CreateDate { get; set; }
        public long SenderCityId { get; set; }
        public long RecipientCityId { get; set; }

        public bool Finished { get; set; }

        public double Amount { get; set; }

        public long? TruckId { get; set; }
    }
}
