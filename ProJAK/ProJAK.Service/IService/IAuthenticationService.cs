using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.AuthenticationDto;

namespace ProJAK.Service.IService
{
    public interface IAuthenticationService
    {
        Task<Response<object>> RegisterUserAsync(RegisterUserDto registerUserDto);
        Task<Response<object>> AddAdminAsync(RegisterUserDto addAdminDto);
        Task<Response<List<GetUserDto>>> GetAllUserAsync();
        Task<Response<List<GetUserDto>>> GetAllAdminAsync();
        Task<AuthDto> LoginAsync(LoginUserDto loginUserDto);
        Task<AuthDto> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
        Task<Response<string>> LogoutAsync();
        Task<Response<object>> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string userId);


    }
}
