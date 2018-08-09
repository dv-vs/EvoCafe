using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;

namespace EvoCafe.DAL.Repositories
{
    public class MenuRepository : BasicRepository<Menu>, IMenues
    {
        public MenuRepository(CafeContext cafeContext) : base(cafeContext)
        {
        }
    }
}
