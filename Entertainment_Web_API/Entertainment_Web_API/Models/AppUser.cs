using Microsoft.AspNetCore.Identity;

namespace Entertainment_Web_API.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? UserNamee { get; set; }
    }
}
