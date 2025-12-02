using PracticeV1.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Business.Auth
{
     public interface IAuthRepository
    {
        Task<AuthMessDTO> LoginAsync(LoginDTO loginDTO);
        Task<AuthMessDTO> RegisterAsync(RegisterDTO registerDTO);
        Task<AuthMessDTO> RefreshTokenAsync(TokenDTO tokenDTO);
        Task LogoutAsync();
    }
}
