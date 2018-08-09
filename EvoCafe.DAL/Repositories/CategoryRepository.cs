using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;

namespace EvoCafe.DAL.Repositories
{
    public class CategoryRepository : BasicRepository<Category>, ICategories
    {
        public CategoryRepository(CafeContext cafeContext) : base(cafeContext)
        {
        }
    }
}
