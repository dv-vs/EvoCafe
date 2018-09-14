using System;
using System.Collections.Generic;

namespace EvoCafe.DAL.Models
{
    public class Order: EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public int ProfileId { get; set; }
        public virtual Profile Profile { get; set; }
        public ICollection<OrderDishes> OrderDishes{ get; set; }
        public decimal Amount { get; set; }
        public Order() => OrderDishes = new List<OrderDishes>();
    }
}
