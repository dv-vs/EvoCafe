using Microsoft.AspNet.Identity;
using UserStore.DAL.Models;

namespace UserStore.DAL.Identity
{
    public class ApplicationUserManager: UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> userStore) : base(userStore) { }
    }
}
