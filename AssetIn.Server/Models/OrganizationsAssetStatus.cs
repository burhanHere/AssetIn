using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetStatus
{
    [Key]
    public int OrganizationsAssetStatusID { get; set; }
    [Required]
    public string OrganizationsAssetStatusName { get; set; }
    [Required]
    public int OrganizationsID { get; set; }
    [ForeignKey("OrganizationsID")]
    public Organization Organization { get; set; }
}
