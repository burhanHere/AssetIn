namespace AssetIn.Server.DTOs;

public class VendorProductDTO
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Model { get; set; }
    public IFormFile? ProfilePicture { get; set; }
    public int VendorID { get; set; }
}