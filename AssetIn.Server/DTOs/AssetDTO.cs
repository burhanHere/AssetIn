namespace AssetIn.Server.DTOs;

public class AssetDTO
{
    public int AssetlD { get; set; }
    public string AssetName { get; set; }
    public string Description { get; set; }
    public string SerialNumber { get; set; }
    public string Barcode { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public DateTime PurchaseDate { get; set; }
    public string Location { get; set; }
    public float DepreciationRate { get; set; }
    public string Problem { get; set; }
    public string AssetIdentificationNumber { get; set; }
    public int OrganizationID { get; set; }
    public int AssetStatusID { get; set; }
    public int AssetCatagoryID { get; set; }
    public int AssetTypeID { get; set; }
}