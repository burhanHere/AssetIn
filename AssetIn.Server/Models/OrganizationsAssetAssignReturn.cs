using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetAssignReturn
{
    [Key]
    public int ID { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public string Notes { get; set; }
    [Required]
    public int AssignedToUserID { get; set; }
    [Required]
    public int AssignedByUserID { get; set; }
    [Required]
    public int AssetID { get; set; }
    [ForeignKey("AssignedToUserID")]
    public User AssignedToUser { get; set; }
    [ForeignKey("AssignedByUserID")]
    public User AssignedByUser { get; set; }
    [ForeignKey("AssetID")]
    public Asset Asset { get; set; }
}
