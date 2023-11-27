using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GAPResource.Data
{
    public class IdNameDTO : IdDTO
    {
        public string Name { get; set; }
    }



    public class IdDTO : DTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
