using EvoCafe.DAL.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace EvoCafe.DAL.Interfaces
{
    public interface IRepository<T> where T: EntityBase
    {
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T GetSingle(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
