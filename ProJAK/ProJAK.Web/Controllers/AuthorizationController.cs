using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProJAK.Domain.Enum;
using ProJAK.Service.DataTransferObject.AuthenticationDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        #region fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IHelpureService _helpureService;
        #endregion

        #region ctor
        public AuthorizationController(IAuthenticationService authenticationService, IHelpureService helpureService)
        {
            _authenticationService = authenticationService;
            _helpureService = helpureService;
        }
        #endregion

        #region RegisterUser
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            var response = await _authenticationService.RegisterUserAsync(registerUserDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region AddAdmin
        [Authorize(Roles = nameof(UserType.Admin))]
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(RegisterUserDto addAdminDto)
        {
            var response = await _authenticationService.AddAdminAsync(addAdminDto);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetAllUser
        [Authorize(Roles = nameof(UserType.Admin))]
        [HttpGet("alluser")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _authenticationService.GetAllUserAsync();

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetAllAdmin
        [Authorize(Roles = nameof(UserType.Admin))]
        [HttpGet("alladmin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var response = await _authenticationService.GetAllAdminAsync();

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region GetUserByUserId
        [Authorize]
        [HttpGet("Profile")]
        public async Task<IActionResult> GetUserByUserId()
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _authenticationService.GetUserByUserIdAsync(currentUserId);

            return StatusCode(response.StatusCode, response);
        }
        #endregion

        #region Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _authenticationService.LoginAsync(loginUserDto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }
        #endregion

        #region RefreshToken
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authenticationService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }
        #endregion

        #region RevokeToken
        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken(RevokeTokenDto revokeToken)
        {
            var token = revokeToken.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required!");

            var result = await _authenticationService.RevokeTokenAsync(token);

            if (!result)
                return BadRequest("Token is invalid!");

            Response.Cookies.Delete("refreshToken");
            return Ok();
        }
        #endregion

        #region ChangePassword
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var currentUserId = await _helpureService.GetUserAsync(User);
            if (currentUserId == null)
            {
                return Unauthorized();
            }
            var response = await _authenticationService.ChangePasswordAsync(changePasswordDto, currentUserId);
            return StatusCode(response.StatusCode, response);

        }
        #endregion

        #region LogoutUser
        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutUser()
        {
            var response = await _authenticationService.LogoutAsync();

            return StatusCode(response.StatusCode, response);
        }
        #endregion
    }
}
