using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Models;
using EvoCafe.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvoCafe.DAL
{
    public class UnitOfWork: IDisposable, IUnitOfWork
    {
        private CafeContext _dbContext;

        private IDishes _dishes;
        private ICategories _categories;
        private IMenues _menues;
        
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();


        public UnitOfWork(CafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            _dbContext.SaveChangesAsync();
        }

        public IDishes Dishes
        {
            get
            {
                if (_dishes == null)
                    _dishes = new DishRepository(_dbContext);

                return _dishes;
            }
        }

        public ICategories Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new CategoryRepository(_dbContext);

                return _categories;
            }
        }

        public IMenues Menues
        {
            get
            {
                if (_menues == null)
                    _menues = new MenuRepository(_dbContext);

                return _menues;
            }
        }

        public IRepository<T> Repository<T>() where T : EntityBase
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
            {
                return repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repo = new BasicRepository<T>(_dbContext);
            repositories.Add(typeof(T), repo);
            return repo;
        }
        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
