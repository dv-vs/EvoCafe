using EvoCafe.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Menu.BLL.Models
{
    public class MenuDishes
    {
        [Display(Name = "?")]
        public bool IsChosen { get; set; }
        [Display(Name = "Блюдо")]
        public Dish Dish { get; set; }
    }

    public class MenuCreateModel: BaseViewModel
    {
        public IEnumerable<MenuDishes> MenuDishes { get; set; }

        public MenuCreateModel() : base() { }

        public MenuCreateModel(IEnumerable<MenuDishes> menuDishes): base()
        {
            MenuDishes = menuDishes;
        }
    }
}
