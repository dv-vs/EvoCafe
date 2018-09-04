using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EvoCafe.DAL.Models
{
    public class Dish: EntityBase
    {
        [Required(ErrorMessage = "Название обязательно")]
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание/компоненты")]
        public string Description { get; set; }
        public byte[] Image { get; set; }
        [Range(1, 500, ErrorMessage = "Недопустимая цена")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Категория обязательна")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Menu> Menues { get; set; }
    }
}
