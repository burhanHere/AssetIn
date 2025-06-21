namespace AssetIn.Server.DTOs;

public class AssetRetireDTO
{
    public string RetirementReason { get; set; }
    public DateTime RetirementDate { get; set; }
    public string Condition { get; set; }
    public int AssetID { get; set; }
    public int OrganizationID { get; set; }
}