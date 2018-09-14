using EvoCafe.DAL.Models;
using System.Data.Entity;

namespace EvoCafe.DAL
{
    public partial class CafeContext: DbContext
    {
        public CafeContext(): base("DBConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<CafeContext>(null);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDishes> OrderDishes { get; set; }
        public DbSet<Menu> Menues { get; set; }
    }
}
