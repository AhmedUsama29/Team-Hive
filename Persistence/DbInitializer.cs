using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer(RoleManager<IdentityRole> _roleManager,
                               UserManager<ApplicationUser> _userManager) : IDbInitializer
    {
        public async Task InitializeIdentityAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Student"));
                    await _roleManager.CreateAsync(new IdentityRole("Teacher"));
                    await _roleManager.CreateAsync(new IdentityRole("TeacherAssistant"));
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                }

                if (!_userManager.Users.Any())
                {
                    var admin = new ApplicationUser()
                    {
                        FirstName = "Admin",
                        LastName = "Ahmed",
                        UserName = "admin",
                        Email = "admin@gmail.com",
                    };

                    await _userManager.CreateAsync(admin, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(admin,"admin");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
