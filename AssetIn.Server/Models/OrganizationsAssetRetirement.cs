using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetRetirement
{
    [Key]
    public int OrganizationsAssetRetirementID { get; set; }
    [Required]
    public string RetirementReason { get; set; }
    [Required]
    public DateTime RetirementDate { get; set; }
    [Required]
    public string Condition { get; set; }
    [Required]
    public int AssetID { get; set; }
    [ForeignKey("AssetID")]
    public Asset Asset { get; set; }
}
