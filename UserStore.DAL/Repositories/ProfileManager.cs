using System;
using EvoCafe.DAL.Models;
using UserStore.DAL.Interfaces;

namespace UserStore.DAL.Repositories
{
    public class ProfileManager : IProfileManager
    {
        UserContext _db;

        public ProfileManager(UserContext db)
        {
            _db = db;
        }

        public void Create(Profile profile)
        {
            _db.Profiles.Add(profile);
            _db.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; 

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
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
