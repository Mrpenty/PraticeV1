using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PracticeV1.Business.DTO;
using PracticeV1.Business.Token;
using PracticeV1.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Business.Auth
{
    public class AuthRepository : IAuthRepository
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<AuthRepository> _logger;

        public AuthRepository( UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor, ITokenRepository tokenRepository, ILogger<AuthRepository> logger
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        public async Task<AuthMessDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return new AuthMessDTO { IsAuthSuccessful = false, ErrorMessage = "email not found" };
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);
            if (result.Succeeded)
            {
                var tokenDto = await _tokenRepository.CreateJWTTokenAsync(user, true);
                var role = await _userManager.GetRolesAsync(user);

                _tokenRepository.SetTokenCookie(tokenDto, _httpContextAccessor.HttpContext);
                return new AuthMessDTO { IsAuthSuccessful = true, ErrorMessage = "Login successful", Id = user.Id, Email = user.Email, Roles = role, AccessToken = tokenDto.AccessToken, RefreshToken = tokenDto.RefreshToken };

            }
            else
            {
                return new AuthMessDTO { IsAuthSuccessful = false, ErrorMessage = "Login Fails" };
            }
        }
        public async Task LogoutAsync()
        {
            _tokenRepository.DeleteTokenCookie(_httpContextAccessor.HttpContext);
            await _signInManager.SignOutAsync();
        }

        public async Task<AuthMessDTO> RefreshTokenAsync(TokenDTO tokenDTO)
        {
            try
            {
                var tokenResult = await _tokenRepository.RefreshJWTTokenAsync(tokenDTO);

                _tokenRepository.SetTokenCookie(tokenResult, _httpContextAccessor.HttpContext);

                return new AuthMessDTO
                {
                    IsAuthSuccessful = true,
                    AccessToken = tokenResult.AccessToken,
                    RefreshToken = tokenResult.RefreshToken,
                };
            }
            catch (Exception)
            {
                return new AuthMessDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "failed"
                };
            }
        }

        public async Task<AuthMessDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new User
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
                NormalizedUserName = _userManager.NormalizeName(registerDTO.UserName),
                NormalizedEmail = _userManager.NormalizeEmail(registerDTO.Email),
                SecurityStamp = Guid.NewGuid().ToString(),


            };
            var createdUser = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!createdUser.Succeeded)
            {
                return new AuthMessDTO { IsAuthSuccessful = false, ErrorMessage = string.Join(", ", createdUser.Errors.Select(e => e.Description)) };
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");

            if (!roleResult.Succeeded)
            {
                await _userManager.DeleteAsync(user);
                return new AuthMessDTO { IsAuthSuccessful = false, ErrorMessage = string.Join(", ", roleResult.Errors.Select(e => e.Description)) };
            }

            return new AuthMessDTO { IsAuthSuccessful = true, ErrorMessage =" Đăng kí thành công"  };

        }
    }
}
