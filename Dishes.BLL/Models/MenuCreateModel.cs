using EvoCafe.DAL.Models;

namespace Menue.BLL.Models
{
    public class MenuCreateModel
    {
        public bool IsChosen { get; set; }
        public Dish Dish { get; set; }
        //public Category Category { get; set; }
    }
}
