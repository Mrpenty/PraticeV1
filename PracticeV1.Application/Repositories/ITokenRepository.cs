using Microsoft.AspNetCore.Http;
using PracticeV1.Application.DTO.Auth;
using PracticeV1.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeV1.Application.Repository
{
    public interface ITokenRepository
    {
        Task<TokenDTO> CreateJWTTokenAsync(User user, bool populateExp);
        Task<TokenDTO> RefreshJWTTokenAsync(TokenDTO tokenDTO);
        void SetTokenCookie(TokenDTO tokenDTO, HttpContext context);
        void DeleteTokenCookie(HttpContext context);
    }
}
