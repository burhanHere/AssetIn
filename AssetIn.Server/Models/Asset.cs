using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class Asset
{
    [Key]
    public int AssetlD { get; set; }
    [Required]
    public string AssetName { get; set; }
    public string Description { get; set; }
    [Required]
    public string SerialNumber { get; set; }
    [Required]
    public string Model { get; set; }
    [Required]
    public string Manufacturer { get; set; }
    [Required]
    public DateTime CreatedDate { get; set; }
    [Required]
    public DateTime UpdatedDate { get; set; }
    [Required]
    public DateTime PurchaseDate { get; set; }
    [Required]
    public string Location { get; set; }
    public float DepreciationRate { get; set; }
    public string Problem { get; set; }
    public string AssetIdentificationNumber { get; set; }
    [Required]
    public int OrganizationlD { get; set; }
    [Required]
    public int AssetStatuslD { get; set; }
    [Required]
    public int AssetCatagoryID { get; set; }
    [Required]
    public int AssetTypeID { get; set; }

    [ForeignKey("OrganizationlD")]
    public Organization Organization { get; set; }
    [ForeignKey("AssetStatuslD")]
    public OrganizationsAssetStatus OrganizationsAssetStatus { get; set; }
    [ForeignKey("AssetCatagoryID")]
    public OrganizationsAssetCatagory OrganizationsAssetCatagory { get; set; }
    [ForeignKey("AssetTypeID")]
    public OrganizationsAssetType OrganizationsAssetType { get; set; }
}
