using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YourAssetManager.Server.Services;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "VendorPolicy")]
public class VendorManagementController(ApplicationDbContext applicationDbContext, CloudinaryService cloudinaryService) : ControllerBase
{
    private readonly VendorManagementRepository _vendorManagementRepository = new(applicationDbContext, cloudinaryService);

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
                ResponseData = new List<string> { "User data not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.GetVendorInfo(userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPut("CreateUpdateVendorInfo")]
    public async Task<IActionResult> CreateUpdateVendorInfo(VendorDTO newVendorInfo)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User data not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.CreateUpdateVendorInfo(newVendorInfo, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost("CreateVendorProduct")]
    public async Task<IActionResult> CreateVendorProduct(VendorProductDTO newVendorProduct)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User data not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.CreateVendorProduct(newVendorProduct, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet("GetVendorProducts")]
    public async Task<IActionResult> GetVendorProducts()
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User data not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.GetVendorProducts(userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch("UploadVendorProfilePicture")]
    public async Task<IActionResult> UploadVendorProfilePicture(IFormFile file)
    {
        var userId = User.FindFirst("UserId")?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User data not found in token." }
            });
        }

        ApiResponse result = await _vendorManagementRepository.UploadVendorProfilePicture(file, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }
}