using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
		{
			var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
			return result;
		}

		public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal?.Claims(ClaimTypes.Role);
		}

        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return loggedInUserId != null ? (int)Convert.ChangeType(loggedInUserId, typeof(int)) : (int)Convert.ChangeType(0, typeof(int));
        }

        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Name);
        }

        public static string GetUserFullName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.GivenName);
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue(ClaimTypes.Email);
        }  
        
        public static string GetToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("token");
        }
        public static string GetRefreshToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirstValue("refreshToken");
        }
    }
}