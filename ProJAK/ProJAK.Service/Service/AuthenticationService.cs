using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProJAK.Domain.Entities;
using ProJAK.Domain.Enum;
using ProJAK.Repository.IRepository;
using ProJAK.Repository.Repository;
using ProJAK.ResponseHandler.Models;
using ProJAK.Service.DataTransferObject.AuthenticationDto;
using ProJAK.Service.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ProJAK.Service.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        #region fields
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SignInManager<User> _signInManager;
        private readonly JWT _jwt;
        #endregion

        #region ctor

        public AuthenticationService(UserManager<User> userManager, UnitOfWork unitOfWork, SignInManager<User> signInManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _signInManager = signInManager;
            _jwt = jwt.Value;
        }
        #endregion

        #region RegisterUser
        public async Task<Response<object>> RegisterUserAsync(RegisterUserDto registerUserDto)
        {
            try
            {
                User newUser = new User
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                    UserType = UserType.User,
                    PhoneNumber = registerUserDto.PhoneNumber,
                    Address = registerUserDto.Address,
                    UserName = registerUserDto.Email
                };
                IdentityResult result = await _userManager.CreateAsync(newUser, registerUserDto.Password);
                if (result.Succeeded)
                {
                    bool addacart = await AddCart(newUser.Id);
                    if (!addacart)
                    {
                        await _userManager.DeleteAsync(newUser);
                        return Response<object>.BadRequest("Failed to register. Please try again.");
                    }
                    await _userManager.AddToRoleAsync(newUser, "User");
                    return Response<object>.Created("Registration successful.");
                }
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<object>.BadRequest("Failed to register. Please try again.", errors);
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while registering the user.", new List<string> { ex.Message });
            }
        }
        #endregion


        #region AddAdmin
        public async Task<Response<object>> AddAdminAsync(RegisterUserDto addAdminDto)
        {
            try
            {
                User newAdmin = new User
                {
                    Name = addAdminDto.Name,
                    Email = addAdminDto.Email,
                    UserType = UserType.Admin,
                    PhoneNumber = addAdminDto.PhoneNumber,
                    Address = addAdminDto.Address,
                    UserName = addAdminDto.Email
                };
                IdentityResult result = await _userManager.CreateAsync(newAdmin, addAdminDto.Password);
                if (result.Succeeded)
                {
                    bool addacart = await AddCart(newAdmin.Id);
                    if (!addacart)
                    {
                        await _userManager.DeleteAsync(newAdmin);
                        return Response<object>.BadRequest("Failed to add admin. Please try again.");
                    }
                    await _userManager.AddToRoleAsync(newAdmin, "Admin");
                    return Response<object>.Created("Add admin successful.");
                }
                var errors = result.Errors.Select(e => e.Description).ToList();
                return Response<object>.BadRequest("Failed to add admin. Please try again.", errors);
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while add the admin.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region Login
        public async Task<AuthDto> LoginAsync(LoginUserDto loginUserDto)
        {
            AuthDto authDto = new AuthDto();
            var user = await _userManager.FindByNameAsync(loginUserDto.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
            {
                authDto.Message = "Email or Password is incorrect!";
                return authDto;
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginUserDto.Password, false, false);
            if (!result.Succeeded)
            {
                authDto.Message = "Failed To Login Please Try Again";
                return authDto;
            }
            var jwtToken = await CreateJwtToken(user);
            authDto.IsAuthenticated = true;
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authDto.Username = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authDto.Roles = roles.ToList();
            if (user.RefreshTokens.Any(t => t.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.IsActive);
                authDto.RefreshToken = activeRefreshToken.Token;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authDto.RefreshToken = refreshToken.Token;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }

            return authDto;
        }
        #endregion

        #region RefreshToken
        public async Task<AuthDto> RefreshTokenAsync(string token)
        {
            var authDto = new AuthDto();
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authDto.Message = "Invalid token";
                return authDto;
            }
            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
            {
                authDto.Message = "Inactive token";
                return authDto;
            }
            refreshToken.RevokedOn = DateTime.UtcNow;

            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            await _userManager.UpdateAsync(user);

            var jwtToken = await CreateJwtToken(user);
            authDto.IsAuthenticated = true;
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            authDto.Username = user.UserName;
            var roles = await _userManager.GetRolesAsync(user);
            authDto.Roles = roles.ToList();
            authDto.RefreshToken = newRefreshToken.Token;
            return authDto;
        }
        #endregion

        #region RevokeToken
        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);

            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);

            return true;
        }
        #endregion

        #region Logout
        public async Task<Response<string>> LogoutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Response<string>.Success("Logout successful.");
            }
            catch (Exception ex)
            {
                return Response<string>.ServerError("An error occurred while logging out.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllUser
        public async Task<Response<List<GetUserDto>>> GetAllUserAsync()
        {
            try
            {
                var users = await _userManager.Users.Where(u => u.UserType == UserType.User).ToListAsync();
                if (!users.Any())
                {
                    return Response<List<GetUserDto>>.NoContent();
                }
                var userDtos = users.Select(user => new GetUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                }).ToList();

                return Response<List<GetUserDto>>.Success(userDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetUserDto>>.ServerError("An error occurred while retrieving users.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetAllAdmin
        public async Task<Response<List<GetUserDto>>> GetAllAdminAsync()
        {
            try
            {
                var admins = await _userManager.Users.Where(u => u.UserType == UserType.Admin).ToListAsync();
                if (!admins.Any())
                {
                    return Response<List<GetUserDto>>.NoContent();
                }
                var userDtos = admins.Select(admin => new GetUserDto
                {
                    Id = admin.Id,
                    Name = admin.Name,
                    Email = admin.Email,
                    PhoneNumber = admin.PhoneNumber,
                    Address = admin.Address,
                }).ToList();

                return Response<List<GetUserDto>>.Success(userDtos, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<List<GetUserDto>>.ServerError("An error occurred while retrieving admins.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region GetUserByUserId
        public async Task<Response<GetUserDto>> GetUserByUserIdAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Response<GetUserDto>.NotFound();
                }
                var userDto = new GetUserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                };

                return Response<GetUserDto>.Success(userDto, "The data was successfully retrieved.");
            }
            catch (Exception ex)
            {
                return Response<GetUserDto>.ServerError("An error occurred while retrieving user.", new List<string> { ex.Message });

            }
        }
        #endregion

        #region ChangePassword
        public async Task<Response<object>> ChangePasswordAsync(ChangePasswordDto changePasswordDto, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Response<object>.NotFound("User not found.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordDto.OldPassword, changePasswordDto.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    var errorMessages = changePasswordResult.Errors.Select(e => e.Description).ToList();
                    return Response<object>.BadRequest("Password change failed.", errorMessages);
                }

                return Response<object>.Success("Password changed successfully.");
            }
            catch (Exception ex)
            {
                return Response<object>.ServerError("An error occurred while changing the password.", new List<string> { ex.Message });
            }
        }
        #endregion

        #region private methods

        private async Task<JwtSecurityToken> CreateJwtToken(User user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim("userId", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            using var generator = new RNGCryptoServiceProvider();

            generator.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                ExpiresOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow
            };
        }
        private async Task<bool> AddCart(string userId)
        {
            Cart newCart = new Cart
            {
                UserId = userId
            };
            var result = await _unitOfWork.Carts.AddAsync(newCart);
            if (result == null)
            {
                return false;
            }

            var save = await _unitOfWork.SaveAsync();
            if (!save)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
