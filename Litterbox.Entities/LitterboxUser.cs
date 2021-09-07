using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Litterbox.Entities
{
    public class LitterboxUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }

        public int? PictureID { get; set; }
        public virtual Picture Picture { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Microsoft.AspNet.Identity.UserManager<LitterboxUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim("Email", Email));
            userIdentity.AddClaim(new Claim("Picture", this.Picture != null ? this.Picture.URL : string.Empty));

            return userIdentity;
        }
    }
}