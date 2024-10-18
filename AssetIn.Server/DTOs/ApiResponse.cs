namespace AssetIn.Server.DTOs
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public object? Errors { get; set; }
        public object? ResponseData { get; set; }
    }
}
