namespace AssetIn.Server.DTOs;

public class FulfillAssetRequestDTO
{
    public int AssetRequestId { get; set; }
    public int AssetID { get; set; }
    public string Notes { get; set; }
}