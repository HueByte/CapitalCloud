using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Common.lib.Types;

namespace API.Configuration
{
    public class RoleSeed
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleSeed(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
         public async Task CreateBasicRoles()
        {
            // var resultadmin = await _roleManager.FindByNameAsync("admin");
            var resultadmin = await _roleManager.FindByNameAsync(Roles.Admin);
            if(resultadmin == null) await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "admin",
            });
            var resultuser = await _roleManager.FindByNameAsync(Roles.User);
            if(resultuser == null) await _roleManager.CreateAsync(new ApplicationRole()
            {
                Name = "user",
            });
        }
    }
}