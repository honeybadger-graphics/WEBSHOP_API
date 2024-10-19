using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEBSHOP_API.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CartId { get; set; }
        public List<int> ProductIds { get; set; } = [];
        public List<int> ProductCount { get; set; } = [];
    }

}
