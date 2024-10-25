using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetCatagory
{
    [Key]
    public int OrganizationsAssetCatagoryID { get; set; }
    [Required]
    public string OrganizationsAssetCatagoryName { get; set; }
    [Required]
    public int OrganizationsID { get; set; }
    [ForeignKey("OrganizationsID")]
    public Organization Organization { get; set; }
}
