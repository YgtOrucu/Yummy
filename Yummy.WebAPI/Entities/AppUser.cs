using Microsoft.AspNetCore.Identity;

namespace Yummy.WebAPI.Entities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? AvatarUrl { get; set; }
        public int ConfirmCode { get; set; }
    }
}
