using System.ComponentModel.DataAnnotations;

namespace PracticeV1.Business.DTO
{
    public class RegisterDTO
    {
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } 

        [Required]
        public string UserName { get; set; } 

        [Required]
        public string Password { get; set; } 

    }
}
