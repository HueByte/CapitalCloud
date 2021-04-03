using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
    public class FirstConfiguration
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public FirstConfiguration(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
         public async Task CreateBasicRoles()
        {
            var resultadmin = await _roleManager.FindByNameAsync("admin");
            if(resultadmin == null) await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "admin",
            });
            var resultuser = await _roleManager.FindByNameAsync("user");
            if(resultuser == null) await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "user",
            });
        }
    }
}