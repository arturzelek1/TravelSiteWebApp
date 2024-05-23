using Microsoft.AspNetCore.Identity;

namespace TravelSiteWeb.Services
{
    
    public class UserService
    {
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task UserFind()
        { 
            string email = "admin@admin.com";
            string password = "Admin1234!";


            var user = await _userManager.FindByEmailAsync(email);

            if(user == null)
            {
                user = new IdentityUser
                {
                    UserName = email,
                    Email = email
                };

                await _userManager.CreateAsync(user, password);
                await _userManager.AddToRoleAsync(user, "Admin"); //Adding the user to the Admin role
            }
            //Checking if the user already exists but has a different password
            else if (!await _userManager.CheckPasswordAsync(user, password))
            {
                await _userManager.AddPasswordAsync(user, password);
            }
        }
    }

}
