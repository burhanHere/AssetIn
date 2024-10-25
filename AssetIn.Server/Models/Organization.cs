namespace AssetIn.Server.Models;

public class Organization
{
    public int OrganizationID { get; set; }
    public string OrganizationName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public string IndustryType { get; set; }
}
