using Microsoft.AspNetCore.Identity;

namespace PracticeV1.Domain.Entity
{
    public class User : IdentityUser<int>
    {
        //public string Email { get;  set; }
        //public string UserName { get;  set; }
        public string? FullName { get;  set; }
        //public List<string> Roles { get;  set; }
        public string? RefreshToken { get; set; } 
        public DateTime? RefreshTokenExpiryTime { get; set; } 
    }

}
