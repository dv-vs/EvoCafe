namespace EvoCafe.DAL.Models
{
    public class Dish: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; } 
        public virtual Category Category { get; set; }
    }
}
