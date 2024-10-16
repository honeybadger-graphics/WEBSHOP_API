using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEBSHOP_API.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CartId { get; set; }
        public List<int>? ProductsId { get; set; }
        public List<int>? ProductsCounts { get; set; }
    }

}
