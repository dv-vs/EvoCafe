using EvoCafe.DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserStore.BLL.DTO;
using UserStore.BLL.Infrastucture;
using UserStore.BLL.Interfaces;
using UserStore.DAL.Interfaces;
using UserStore.DAL.Models;

namespace UserStore.BLL.Services
{
    public class UserService : IUserService
    {
        IIdentityUnitOfWork _identityUnitOfWork;

        public UserService(IIdentityUnitOfWork identityUnitOfWork)
        {
            _identityUnitOfWork = identityUnitOfWork;
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            ApplicationUser user = await _identityUnitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            
            if (user != null)
                claim = await _identityUnitOfWork.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            return claim;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await _identityUnitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await _identityUnitOfWork.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                
                await _identityUnitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                Profile clientProfile = new Profile { Id = user.Id };
                _identityUnitOfWork.ProfileManager.Create(clientProfile);
                await _identityUnitOfWork.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task SetInitialData(UserDTO adminDto, IEnumerable<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _identityUnitOfWork.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole { Name = roleName };
                    await _identityUnitOfWork.RoleManager.CreateAsync(role);
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    _identityUnitOfWork.Dispose();
                
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
