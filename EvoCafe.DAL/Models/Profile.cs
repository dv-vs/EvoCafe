using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EvoCafe.DAL.Models
{
    public class Profile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public Profile()
        {
            Orders = new List<Order>();
        }
    }
}
