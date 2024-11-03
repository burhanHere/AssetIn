using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.DTOs;

public class UserSignInDTO
{
    [Required(ErrorMessage = "User Email is required")]
    public required string? Email { get; set; }
    [Required(ErrorMessage = "User Password is required")]
    public required string? Password { get; set; }
}
