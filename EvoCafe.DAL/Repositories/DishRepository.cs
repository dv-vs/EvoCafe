using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;

namespace EvoCafe.DAL.Repositories
{
    public class DishRepository : BasicRepository<Dish>, IDishes
    {
        public DishRepository(CafeContext cafeContext) : base(cafeContext)
        {
        }
    }
}
