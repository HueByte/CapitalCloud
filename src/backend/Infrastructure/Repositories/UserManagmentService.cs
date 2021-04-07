using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Repositories
{
    public class UserManagmentService : IUserManagmentService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserManagmentService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task<ServiceResponse<List<string>>> GetUsersUrlById(List<string> idList)
        {
            var avatarsUrlList = new List<string>();
            foreach (var id in idList)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    var userAvatar = user.Avatar_Url ?? new string("https://www.viadelvino.com/wp-content/uploads/2016/02/photo.jpg.png");
                    avatarsUrlList.Add(userAvatar);
                }
                else
                avatarsUrlList.Add(string.Empty);

            }
            if (avatarsUrlList == null) return StaticResponse<List<string>>.BadResponse(null, "No user founded", 1);
            else return StaticResponse<List<string>>.GoodResponse(avatarsUrlList, "Loaded Avatars_Urls");
        }
    }
}