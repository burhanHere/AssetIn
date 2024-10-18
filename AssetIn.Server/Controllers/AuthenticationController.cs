using AssetIn.Server.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers
{
    [ApiController]
    [Route("AssetIn.Server/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet(Name = "Testing")]
        public ApiResponse Get() => new ApiResponse()
        {
            Status = 200,
            ResponseData = "All Good."
        };
    }
}
