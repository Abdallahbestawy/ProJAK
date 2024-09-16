using Microsoft.AspNetCore.Mvc;
using ProJAK.Service.DataTransferObject.AuthenticationDto;
using ProJAK.Service.IService;

namespace ProJAK.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthorizationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(RegisterUserDto registerUserDto)
        {
            var response = await _authenticationService.RegisterUserAsync(registerUserDto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("AddAdmin")]
        public async Task<IActionResult> AddAdmin(RegisterUserDto addAdminDto)
        {
            var response = await _authenticationService.AddAdminAsync(addAdminDto);

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("alluser")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _authenticationService.GetAllUserAsync();

            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("alladmin")]
        public async Task<IActionResult> GetAllAdmin()
        {
            var response = await _authenticationService.GetAllAdminAsync();

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto loginUserDto)
        {
            var result = await _authenticationService.LoginAsync(loginUserDto);

            if (!result.IsAuthenticated)
                return BadRequest(result.Message);
            return Ok(result);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var result = await _authenticationService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);
            return Ok(result);
        }
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
        [HttpPost("Logout")]
        public async Task<IActionResult> LogoutUser()
        {
            var response = await _authenticationService.LogoutAsync();

            return StatusCode(response.StatusCode, response);
        }
    }
}
