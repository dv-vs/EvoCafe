using System.Collections.Generic;

namespace EvoCafe.DAL.Models
{
    public class Category: EntityBase
    {
        public string Name { get; set; }
        public ICollection<Dish> Dishes { get; set; }
    }
}
