using Microsoft.AspNetCore.Identity;

namespace AssetIn.Server.Models
{
    public class User : IdentityUser
    {
        public string? ProfilePicturePath { get; set; }
        public bool Status { get; set; } = true;
        public int? OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
