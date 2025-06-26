namespace AssetIn.Server.DTOs;

public class UpdateEmployeeDTO
{
    public string Id { get; set; }
    public int OrganizationId { get; set; }
    public string userName { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}