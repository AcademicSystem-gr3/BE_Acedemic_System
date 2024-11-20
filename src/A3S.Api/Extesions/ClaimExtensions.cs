using A3S.Core.Domain.Constant;
using A3S.Core.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.Reflection;
using System.Security.Claims;

namespace A3S.Api.Extesions
{
    public static class ClaimExtensions
    {
        public static void GetPermissions(this List<RoleClaimsDto> allPermissions, Type policy)
        {
            FieldInfo[] fields = policy.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (FieldInfo fi in fields)
            {
                var descriptionAttributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);

                if (descriptionAttributes.Length > 0)
                {
                    var description = (DescriptionAttribute)descriptionAttributes[0];
                    string displayName = description.Description;

                    allPermissions.Add(new RoleClaimsDto
                    {
                        Value = fi.GetValue(null).ToString(),
                        Type = "Permission",
                        DisplayName = displayName
                    });
                }
            }
        }

        public static async Task AddPermissionClaim(this RoleManager<Role> roleManager, Role role, string permission)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);

            bool hasPermissionClaim = allClaims.Any(a => a.Type == "Permission" && a.Value == permission);

            if (!hasPermissionClaim)
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
