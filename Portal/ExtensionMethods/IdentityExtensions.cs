using System.Security.Claims;

namespace Portal.ExtensionMethods
{
    public static class IdentityExtensions
    {
        public static string GetRole(this ClaimsPrincipal user)
        {
            var isStudent = user.HasClaim("Role", "Student");
            var isCanteenEmployee = user.HasClaim("Role", "CanteenEmployee");

            if (isStudent)
            {
                return "Student";
            }
            else if (isCanteenEmployee)
            {
                return "CanteenEmployee";
            }
            else
            {
                return "None";
            }
        }
    }
}
