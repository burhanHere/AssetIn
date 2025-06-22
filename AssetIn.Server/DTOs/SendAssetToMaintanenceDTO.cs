namespace AssetIn.Server.DTOs;

public class SendAssetToMaintanenceDTO
{
    public string Problem { get; set; }
    public int AssetID { get; set; }
    public int OrganizationID { get; set; }
}
