using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStore.DAL.Models;

namespace UserStore.DAL.Identity
{
    public class ApplicationRoleManager: RoleManager<UserRole>
    {
        public ApplicationRoleManager(RoleStore<UserRole> store) : base(store) { }
    }
}
