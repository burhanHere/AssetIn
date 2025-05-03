using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "VendorPolicy")]
public class VendorManagementController(ApplicationDbContext applicationDbContext) : ControllerBase
{
    private readonly VendorManagementRepository _vendorManagementRepository = new(applicationDbContext);

    [HttpGet("GetVendorInfo")]
    public async Task<IActionResult> GetVendorInfo()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.GetVendorInfo(userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost("CreateVendorInfo")]
    public async Task<IActionResult> CreateVendorInfo(VendorDTO newVendorInfo)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.CreateVendorInfo(newVendorInfo, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch("UpdateVendorInfo")]
    public async Task<IActionResult> UpdateVendorInfo(VendorDTO newVendorInfo)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.UpdateVendorInfo(newVendorInfo, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

}