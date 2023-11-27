using System;

namespace GAPResource.Models
{
    public class IndexSenderRecipientViewModel: IndexSenderViewModel
    {
        public IEnumerable<CityViewModel> RecipientCity { get; set; } = new List<CityViewModel>();
    }


    public class IndexSenderViewModel
    {
        public IEnumerable<CityViewModel> SenderCity { get; set; } = new List<CityViewModel>();
    }
}
