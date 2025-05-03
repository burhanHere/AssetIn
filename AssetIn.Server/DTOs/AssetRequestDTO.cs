namespace AssetIn.Server.DTOs;

public class AssetRequestDTO
{
    public int OrganizationsAssetRequestID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int OrganizationID { get; set; }
}
