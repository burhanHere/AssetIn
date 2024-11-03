using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.DTOs;

public class ResetPasswordDTO
{
    [Required(ErrorMessage = "New Password is required.")]
    public required string? NewPassword { get; set; }
    [Required(ErrorMessage = "Email is required.")]
    public required string? Email { get; set; }
    [Required(ErrorMessage = "Password Reset is required.")]
    public required string? Token { get; set; }
}
