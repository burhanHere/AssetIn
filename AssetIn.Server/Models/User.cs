using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using NuGet.Common;

namespace AssetIn.Server.Models
{
    public class User : IdentityUser
    {
        public string? ProfilePicturePath { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int? OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public Organization Organization { get; set; }
    }
}
