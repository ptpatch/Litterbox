using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Litterbox.Entities;
using Litterbox.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LitterBoxMVC.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class LitterboxUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<LitterboxUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        internal Task<ClaimsIdentity> GenerateUserIdentityAsync(LitterboxUserManager litterboxUserManager)
        {
            throw new NotImplementedException();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<LitterboxUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}