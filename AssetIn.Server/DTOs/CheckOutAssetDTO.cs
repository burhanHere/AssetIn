namespace AssetIn.Server.DTOs;

public class CheckOutAssetDTO
{
    public int AssetId { get; set; }
    public string AssignedToUserId { get; set; }
    public string AssignedByUserId { get; set; }
    public int OrganizationID { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string? Notes { get; set; }
}