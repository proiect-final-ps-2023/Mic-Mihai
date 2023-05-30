using Microsoft.AspNetCore.Identity;

namespace PhotoGalleryApp.Models
{
    public class AdminUserSeed
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUserSeed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var adminRole = new IdentityRole("Admin");

            if (!await _roleManager.RoleExistsAsync(adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }

            var user = await _userManager.FindByEmailAsync("admin@yourwebsite.com");

            if (user == null)
            {
                user = new User
                {
                    UserName = "admin",
                    Email = "admin@yourwebsite.com"
                };

                var result = await _userManager.CreateAsync(user, "Admin123!");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, adminRole.Name);
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);
                }
            }
        }
    }
}
