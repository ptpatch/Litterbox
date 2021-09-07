using LitterBox.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Litterbox.Services
{
    public class LitterboxRoleManager : RoleManager<IdentityRole>
    {
        public LitterboxRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }

        public static LitterboxRoleManager Create(IdentityFactoryOptions<LitterboxRoleManager> options, IOwinContext context)
        {
            return new LitterboxRoleManager(new RoleStore<IdentityRole>(context.Get<LitterboxContext>()));
        }
    }
}