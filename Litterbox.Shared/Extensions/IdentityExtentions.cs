using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Litterbox.Shared.Extensions
{
    public static class IdentityExtentions
    {
        public static string GetUserEmail(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Email");

            return (claim != null) ? claim.Value : string.Empty;
        }

        public static string GetUserPicture(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Picture");

            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}