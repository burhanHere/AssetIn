namespace AssetIn.Server.DTOs;

public class NewEmployeeDTO
{
    public int OrganizationId { get; set; }
    public string userName { get; set; }
    public string PhoneNumber { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}