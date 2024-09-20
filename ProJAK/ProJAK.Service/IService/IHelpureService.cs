using System.Security.Claims;

namespace ProJAK.Service.IService
{
    public interface IHelpureService
    {
        public Task<string> GetUserAsync(ClaimsPrincipal user);
    }
}
