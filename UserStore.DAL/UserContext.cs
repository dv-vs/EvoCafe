using System.Data.Entity;
using EvoCafe.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using UserStore.DAL.Models;

namespace UserStore.DAL
{
    public class UserContext : IdentityDbContext<ApplicationUser>
    {
        public UserContext() : base("DBConnection") { }
        public DbSet<Profile> Profiles { get; set; }
    }
}
