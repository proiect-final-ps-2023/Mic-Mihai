using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Xml.Linq;

namespace PhotoGalleryApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReportsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ReportsController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Generate()
        {
            // Get admin role
            var adminRole = await _roleManager.FindByNameAsync("Admin");

            // Get all users
            var allUsers = _userManager.Users.ToList();

            // Filter out admin users
            var nonAdminUsers = allUsers
                .Where(user => !_userManager.IsInRoleAsync(user, adminRole.Name).Result)
                .ToList();

            var xDoc = new XDocument(
                new XElement("Users",
                    from user in nonAdminUsers
                    select new XElement("User",
                        new XElement("Id", user.Id),
                        new XElement("UserName", user.UserName),
                        new XElement("Email", user.Email)
                        )
                    )
                );

            return File(System.Text.Encoding.UTF8.GetBytes(xDoc.ToString()), "application/xml", "UsersReport.xml");
        }

    }
}
