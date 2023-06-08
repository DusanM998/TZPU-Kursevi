using Microsoft.AspNetCore.Identity;

namespace TZPU.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
