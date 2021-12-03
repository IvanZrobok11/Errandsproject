using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Errands.Mvc.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.IsInRole("Administrator");
        }
    }
}
