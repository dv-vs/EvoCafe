using EvoCafe.DAL.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace UserStore.DAL.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual Profile Profile { get; set; }
    }
}
