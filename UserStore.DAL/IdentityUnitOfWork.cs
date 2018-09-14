using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using UserStore.DAL.Identity;
using UserStore.DAL.Interfaces;
using UserStore.DAL.Models;
using UserStore.DAL.Repositories;

namespace UserStore.DAL
{
    public class IdentityUnitOfWork : IIdentityUnitOfWork
    {
        UserContext _db;
        ApplicationUserManager _userManager;
        IProfileManager _profileManager;
        ApplicationRoleManager _roleManager;

        public IdentityUnitOfWork(UserContext db)
        {
            _db = db;
            _userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(_db));
            _roleManager = new ApplicationRoleManager(new RoleStore<UserRole>(_db));
            _profileManager = new ProfileManager(_db);
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
        }

        public IProfileManager ProfileManager
        {
            get { return _profileManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }



        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _userManager.Dispose();
                    _roleManager.Dispose();
                    _profileManager.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
