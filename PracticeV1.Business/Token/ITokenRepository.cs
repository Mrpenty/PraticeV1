using Microsoft.AspNetCore.Http;
using PracticeV1.Business.DTO;
using PracticeV1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Business.Token
{
    public interface ITokenRepository
    {
        Task<TokenDTO> CreateJWTTokenAsync(User user, bool populateExp);
        Task<TokenDTO> RefreshJWTTokenAsync(TokenDTO tokenDTO);
        void SetTokenCookie(TokenDTO tokenDTO, HttpContext context);
        void DeleteTokenCookie(HttpContext context);
    }
}
