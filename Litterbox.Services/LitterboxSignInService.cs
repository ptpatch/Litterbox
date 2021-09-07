using Litterbox.Entities;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Litterbox.Services
{
    public class LitterboxSignInManager : SignInManager<LitterboxUser, string>
    {
        public LitterboxSignInManager(LitterboxUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(LitterboxUser user)
        {
            return user.GenerateUserIdentityAsync((LitterboxUserManager)UserManager);
        }

        public static LitterboxSignInManager Create(IdentityFactoryOptions<LitterboxSignInManager> options, IOwinContext context)
        {
            return new LitterboxSignInManager(context.GetUserManager<LitterboxUserManager>(), context.Authentication);
        }
    }
}