namespace AssetIn.Server.DTOs;

public class UserDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public string ProfilePicturePath { get; set; }
    public bool Status { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfJoining { get; set; }
    public DateTime DateOfBirth { get; set; }
}