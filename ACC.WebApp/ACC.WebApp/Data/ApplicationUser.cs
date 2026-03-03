using Microsoft.AspNetCore.Identity;

namespace ACC.WebApp.Data
{
    public class ApplicationUser : IdentityUser 
    {
        public DateTimeOffset? LastUserNameChangeUtc { get; set; }
    }
}
