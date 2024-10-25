using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class UserOrganizations
{
    [Key]
    public int ID { get; set; }
    [Required]
    public int UserID { get; set; }
    [Required]
    public int OrganizationsID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; }
    [ForeignKey("OrganizationsID")]
    public Organization Organization { get; set; }
}
