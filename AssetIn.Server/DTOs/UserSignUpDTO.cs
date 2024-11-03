using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.DTOs;

public class UserSignUpDTO
{
    [Required(ErrorMessage = "User FullName is required")]
    [StringLength(100)]
    public required string? FullName { get; set; }
    [Required(ErrorMessage = "User UserName is required")]
    [StringLength(50)]
    public required string? UserName { get; set; }
    [Required(ErrorMessage = "User Gender is required")]
    [StringLength(10)]
    public required string? Gender { get; set; }
    [Required(ErrorMessage = "User DateOfBirth is required")]
    public DateTime DateOfBirth { get; set; }
    [Required(ErrorMessage = "User Email is required")]
    [EmailAddress]
    [StringLength(100)]
    public required string? Email { get; set; }
    [Required(ErrorMessage = "User PhoneNumber is required")]
    [StringLength(15)]
    public required string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "User Password is required")]
    [StringLength(255)]
    public required string? Password { get; set; }
    [Required(ErrorMessage = "User Role is required")]
    public required string? RequiredRole { get; set; }
}
