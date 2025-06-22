using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class VendorProduct
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Model { get; set; }
    public string? ProductImage { get; set; }
    [Required]
    public int VendorID { get; set; }
    [ForeignKey("VendorID")]
    public Vendor Vendor { get; set; }
}
