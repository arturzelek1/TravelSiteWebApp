using Microsoft.AspNetCore.Identity;

namespace TravelSiteWeb.Services
{
    public class RolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var roles = new[] { "Admin", "Manager", "User" };
            foreach (var role in roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
    
}
