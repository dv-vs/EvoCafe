using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        private DbSet<T> JoinIncludes(DbSet<T> dbSet, params string[] includes)
        {
            foreach (var include in includes)
                dbSet.Include(include);

            return dbSet;
        }

        public void Create(T item)
        {
            _dbSet.Add(item);
        }

        public async Task Delete(int id)
        {
            var entity = await GetSingleAsync(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        public void Delete(T item)
        {
            _dbSet.Remove(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public Task<T> GetSingleAsync(int id, params string[] includes) => JoinIncludes(_dbSet, includes).FindAsync(id);

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate, params string[] includes) => JoinIncludes(_dbSet, includes).Where(predicate);

        public IQueryable<T> GetAll(params string[] includes) => JoinIncludes(_dbSet, includes).AsNoTracking();

        public void Update(T item)
        {
            //_dbContext.Entry(item).State = EntityState.Modified;
            //_dbSet.Attach(item);
            _dbSet.AddOrUpdate(item);
        }
    }
}
