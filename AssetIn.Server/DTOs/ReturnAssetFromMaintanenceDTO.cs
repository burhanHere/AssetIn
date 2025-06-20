namespace AssetIn.Server.DTOs;

public class ReturnAssetFromMaintanenceDTO
{
    public DateTime RetunDate { get; set; }
    public string MaintanenceResult { get; set; }
    public int AssetID { get; set; }
    public int OrganizationID { get; set; }
    public bool IsRepaired { get; set; }
}
