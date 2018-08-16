using EvoCafe.DAL.Models;
using System.Threading.Tasks;

namespace EvoCafe.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IDishes Dishes { get; }
        ICategories Categories { get; }
        IMenues Menues { get; }

        IRepository<T> Repository<T>() where T : EntityBase;

        Task SaveChangesAsync();
    }
}
