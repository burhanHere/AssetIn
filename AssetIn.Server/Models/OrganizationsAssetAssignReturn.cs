using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetAssignReturn
{
    [Key]
    public int ID { get; set; }
    [Required]
    public DateTime AssignedAt { get; set; }
    [Required]
    public DateTime ReturnedAt { get; set; }
    [Required]
    public string Notes { get; set; }
    [Required]
    public string AssignedToUserID { get; set; }
    [Required]
    public string AssignedByUserID { get; set; }
    public string? CheckInByUserID { get; set; }
    public string CheckInNotes { get; set; }
    [Required]
    public int AssetID { get; set; }
    [ForeignKey("AssignedToUserID")]
    public User AssignedToUser { get; set; }
    [ForeignKey("AssignedByUserID")]
    public User AssignedByUser { get; set; }
    [ForeignKey("CheckInByUserID")]
    public User CheckInByUser { get; set; }
    [ForeignKey("AssetID")]
    public Asset Asset { get; set; }
}
