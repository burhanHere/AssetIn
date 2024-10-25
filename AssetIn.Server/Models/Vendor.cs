using System.ComponentModel.DataAnnotations;

namespace AssetIn.Server.Models;

public class Vendor
{
    [Key]
    public int VendorID { get; set; }
    [Required]
    public string VendorName { get; set; }
    [Required]
    public string OfficeAddress { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string ContactPerson { get; set; }
    [Required]
    public bool Status { get; set; }
}
