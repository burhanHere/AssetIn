using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class UserRole
{
    [Key]
    public int ID { get; set; }
    [Required]
    public DateTime AssignedDate { get; set; }
    [Required]
    public bool RoleStatus { get; set; }
    [Required]
    public int UserID { get; set; }
    [Required]
    public int RoleID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; }
    [ForeignKey("RoleID")]
    public Role Role { get; set; }
}
