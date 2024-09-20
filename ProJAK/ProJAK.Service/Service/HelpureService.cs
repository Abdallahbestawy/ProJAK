using Microsoft.AspNetCore.Identity;
using ProJAK.Domain.Entities;
using ProJAK.Service.IService;
using System.Security.Claims;

namespace ProJAK.Service.Service
{
    public class HelpureService : IHelpureService
    {
        #region fields
        private readonly UserManager<User> _userManager;
        #endregion

        #region ctor
        public HelpureService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        #endregion

        #region GetUser
        public async Task<string> GetUserAsync(ClaimsPrincipal user)
        {
            var userData = await _userManager.GetUserAsync(user);

            if (userData == null)
                return string.Empty;

            return userData.Id;
        }
        #endregion
    }
}
