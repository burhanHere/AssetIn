namespace AssetIn.Server.DTOs;

public class CheckInAssetDTO
{
    public int AssetId { get; set; }
    public string CheckInByUserId { get; set; }
    public int OrganizationID { get; set; }
    public DateTime CheckInDate { get; set; }
    public string? Notes { get; set; }
}