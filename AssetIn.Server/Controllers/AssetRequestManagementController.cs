using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Models;
using AssetIn.Server.Repositories;
using AssetIn.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]

public class AssetRequestManagementController(ApplicationDbContext applicationDbContext, UserManager<User> userManager, EmailService emailService) : ControllerBase
{
    private readonly AssetRequestManagementRepository _assetRequestManagementRepository = new(applicationDbContext, userManager, emailService);

    [HttpGet("GetAllAssetRequestAdminList")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetAllAssetRequestAdminList(int organizationId)
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

        var result = await _assetRequestManagementRepository.GetAllAssetRequestAdminList(organizationId, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet("GetAllAssetRequestEmployeeListStatsAndDesignatedAssets")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy")]
    public async Task<IActionResult> GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(int organizationId)
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

        var result = await _assetRequestManagementRepository.GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(organizationId, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost("CreateAssetRequest")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy")]
    public async Task<IActionResult> CreateAssetRequest(AssetRequestDTO assetRequestDTO)
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

        var result = await _assetRequestManagementRepository.CreateAssetRequest(assetRequestDTO, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch("UpdateAssetRequestStatusToAccepted")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAssetRequestStatusToAccepted(int AssetRequestID)
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

        var result = await _assetRequestManagementRepository.UpdateAssetRequestStatus(AssetRequestID, 1, userId);
        return HelperFunctions.ResponseFormatter(this, result);

    }
    [HttpPatch("UpdateAssetRequestStatusToDeclined")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAssetRequestStatusToDeclined(int AssetRequestID)
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

        var result = await _assetRequestManagementRepository.UpdateAssetRequestStatus(AssetRequestID, 3, userId);
        return HelperFunctions.ResponseFormatter(this, result);

    }
    [HttpPatch("UpdateAssetRequestStatusToFulfilled")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> UpdateAssetRequestStatusToFulfilled(int AssetRequestID)
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

        var result = await _assetRequestManagementRepository.UpdateAssetRequestStatus(AssetRequestID, 4, userId);
        return HelperFunctions.ResponseFormatter(this, result);

    }
    [HttpPatch("UpdateAssetRequestStatusToCanceled")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy")]
    public async Task<IActionResult> UpdateAssetRequestStatusToCanceled(int AssetRequestID)
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

        var result = await _assetRequestManagementRepository.UpdateAssetRequestStatus(AssetRequestID, 5, userId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

}