using System;
using System.Threading.Tasks;
using UserStore.DAL.Identity;

namespace UserStore.DAL.Interfaces
{
    public interface IIdentityUnitOfWork: IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IProfileManager ProfileManager { get; }
        ApplicationRoleManager RoleManager { get; }
        Task SaveAsync();
    }
}
