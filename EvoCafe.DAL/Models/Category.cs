using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvoCafe.DAL.Models
{
    public class Category: EntityBase
    {
        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name= "Название")]
        public string Name { get; set; }
        public virtual ICollection<Dish> Dishes { get; set; }
    }
}
