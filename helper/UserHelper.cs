using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace SocialClint.helper
{
    public static class UserHelper
    {

        public static string GetUserRequestId(this ClaimsPrincipal user)
        {
           var UidClaim=user.FindFirst("uid");
            return UidClaim.Value;

        }
    }
}
