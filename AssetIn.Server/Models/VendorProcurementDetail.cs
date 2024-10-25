using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class VendorProcurementDetail
{
    [Key]
    public int ID { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductCatagory { get; set; }
    [Required]
    public string ProductStatus { get; set; }
    [Required]
    public string OrderStatus { get; set; }
    [Required]
    public float ProductPrice { get; set; }
    [Required]
    public float GrandTotal { get; set; }
    [Required]
    public DateTime DispachDate { get; set; }
    [Required]
    public string HardlnvoiceImagePath { get; set; }
    [Required]
    public int VendorID { get; set; }
    [ForeignKey("VendorID")]
    public Vendor Vendor { get; set; }
}
