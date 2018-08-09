using System;
using System.Collections.Generic;

namespace EvoCafe.DAL.Models
{
    public class Order: EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderDishes> OrderDishes{ get; set; }
        public decimal Amount { get; set; }
        public Order() => OrderDishes = new List<OrderDishes>();
    }
}
