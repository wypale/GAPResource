using System.ComponentModel.DataAnnotations;

namespace GAPResource.Models
{
    public class SendOrderViewModel
    {
        [Required()]
        public long SenderCityId { get; set; }

    }
}
