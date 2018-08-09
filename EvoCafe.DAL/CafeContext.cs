using EvoCafe.DAL.Models;
using System.Data.Entity;

namespace EvoCafe.DAL
{
    public class CafeContext: DbContext
    {
        public CafeContext(): base("DBConnection") { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDishes> OrderDishes { get; set; }
        public DbSet<Menu> Menues { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
