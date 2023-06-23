using Microsoft.AspNetCore.Identity;

namespace AVN.Model.Entities
{
    public class AppUser : IdentityUser
    {
        public string? PhoneNumber { get; set; }
        public string? GradeBookNumber { get; set; }
        public string FullName { get; set; }
    }
}
