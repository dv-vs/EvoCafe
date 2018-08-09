using EvoCafe.DAL.Interfaces;
using EvoCafe.DAL.Repositories;
using System;

namespace EvoCafe.DAL
{
    public class UnitOfWork: IDisposable, IUnitOfWork
    {
        private CafeContext _dbContext;

        private IDishes _dishes;
        private ICategories _categories;
        private IMenues _menues;

        public UnitOfWork(CafeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SaveChanges()
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
