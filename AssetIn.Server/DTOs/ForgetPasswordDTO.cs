using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.DTOs;

public class ForgetPasswordDTO
{
    [Required(ErrorMessage = "User Email is required")]
    public required string Email { get; set; }
}
