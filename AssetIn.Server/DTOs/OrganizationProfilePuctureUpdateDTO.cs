namespace AssetIn.Server.DTOs;

public class OrganizationProfilePictureUpdateDTO {
    public IFormFile file { get; set; }
    public int organizationId { get; set; }
}