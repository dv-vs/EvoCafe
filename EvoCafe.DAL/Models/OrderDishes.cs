namespace EvoCafe.DAL.Models
{
    public class OrderDishes: EntityBase
    {
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int DishId{ get; set; }
        public Dish Dish { get; set; }
        public decimal Price { get; set; }
    }
}
