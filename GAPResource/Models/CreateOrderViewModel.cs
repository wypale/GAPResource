using System.ComponentModel.DataAnnotations;

namespace GAPResource.Models
{
    public class CreateOrderViewModel
    {
        [Required()]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string? SenderAddress { get; set; }
        [Required()]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.")]
        public string? RecipientAddress { get; set; }
        [Required()]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:n2}")]
        public string? IdWeight { get; set; }
        [Required()]
        [DisplayFormat(DataFormatString = "{dd.MM.yyyy}", ApplyFormatInEditMode = true)]

        public DateTime IdDate { get; set; }
        [Required()]
        public long SenderCityId { get; set; }
        [Required()]
        public long RecipientCityId { get; set; }

    }
}
