using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public bool PhoneNumberConfirmed { get; set; } = false;
        public string? ProfilePicturePath { get; set; }
        public bool Status { get; set; } = true;
    }
}
