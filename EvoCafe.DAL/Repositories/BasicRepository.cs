using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace EvoCafe.DAL.Repositories
{
    public class BasicRepository<T> : IRepository<T> where T : EntityBase
    {
        protected DbSet<T> _dbSet;
        DbContext _dbContext;

        public BasicRepository(CafeContext cafeContext)
        {
            _dbSet     = cafeContext.Set<T>();
            _dbContext = cafeContext;
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public T GetSingle(int id) => _dbSet.Find(id);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        public IQueryable<T> GetAll() => _dbSet;

        public void Update(T item)
        {
            //_dbSet.Attach(item);
            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
