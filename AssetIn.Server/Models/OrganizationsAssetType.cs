using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetType
{
    [Key]
    public int OrganizationsAssetTypeID { get; set; }
    [Required]
    public string OrganizationsAssetTypeName { get; set; }
    [Required]
    public int OrganizationsID { get; set; }
    [ForeignKey("OrganizationsID")]
    public Organization Organization { get; set; }
}
