using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Models;
using AssetIn.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;


[ApiController]
[Route("AssetIn.Server/[controller]")]
public class AssetManagementController(ApplicationDbContext applicationDbContext, UserManager<User> userManager) : ControllerBase
{
    private readonly AssetManagementRepository _assestManagementRepository = new(applicationDbContext, userManager);
    [HttpPost("CreateAsset")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> CreateAsset(AssetDTO newAsset)
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
        var result = await _assestManagementRepository.CreateAsset(newAsset, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "UpdateAsset")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAsset(AssetDTO newAsset)
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
        var result = await _assestManagementRepository.UpdateAsset(newAsset, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpDelete(template: "DeleteAsset")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> DeleteAsset(int assetID)
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
        var result = await _assestManagementRepository.DeleteAsset(assetID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "GetAllAsset")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAllAsset(int organizationID)
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
        var result = await _assestManagementRepository.GetAllAsset(organizationID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "GetAsset")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAsset(int assetID)
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
        var result = await _assestManagementRepository.GetAsset(assetID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "CreateNewAssetCatagory")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> CreateNewAssetCatagory(AssetCatagoryDTO newCatagory)
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
        var result = await _assestManagementRepository.CreateNewAssetCatagory(newCatagory, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpDelete(template: "DeleteAssetCatagory")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> DeleteAssetCatagory(int catagoryID)
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

        var result = await _assestManagementRepository.DeleteAssetCatagory(catagoryID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "UpdateAssetCatagory")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAssetCatagory(AssetCatagoryDTO assetCatagory)
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
        var result = await _assestManagementRepository.UpdateAssetCatagory(assetCatagory, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "GetAllAssetCatagory")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAllAssetCatagory(int organizationID)
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
        var result = await _assestManagementRepository.GetAllAssetCatagory(organizationID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "CreateNewAssetType")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> CreateNewAssetType(AssetTypeDTO assetType)
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
        var result = await _assestManagementRepository.CreateNewAssetType(assetType, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpDelete(template: "DeleteAssetType")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> DeleteAssetType(int AssetTypeID)
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
        var result = await _assestManagementRepository.DeleteAssetType(AssetTypeID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "UpdateAssetType")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAssetType(AssetTypeDTO assetType)
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
        var result = await _assestManagementRepository.UpdateAssetType(assetType, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "GetAllAssetType")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAllAssetType(int organizationID)
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
        var result = await _assestManagementRepository.GetAllAssetType(organizationID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet(template: "GetAllAssetStatus")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAllAssetStatus(int organizationID)
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
        var result = await _assestManagementRepository.GetAllAssetStatus(organizationID, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }
}