using PracticeV1.Application.DTO.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Repository
{
     public interface IAuthRepository
    {
        Task<AuthMessDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthMessDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthMessDTO> RefreshTokenAsync(TokenDTO tokenDTO);
        Task LogoutAsync();
    }
}
