using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using AssetIn.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]

public class AssetRequestManagementController(ApplicationDbContext applicationDbContext, UserManager<User> userManager) : ControllerBase
{
    private readonly AssetRequestManagementRepository _assetRequestManagementRepository = new(applicationDbContext, userManager);

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
                ResponseData = new List<string> { "User not found in token." }
            });
        }

        var result = await _assetRequestManagementRepository.GetAllAssetRequestAdminList(organizationId, userId);
        return Ok(result);
    }

    [HttpGet("GetAllAssetRequestEmployeeList")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerOrganizationEmployeePolicy")]
    public async Task<IActionResult> GetAllAssetRequestEmployeeList(int organizationId)
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

        var result = await _assetRequestManagementRepository.GetAllAssetRequestEmployeeList(organizationId, userId);
        return Ok(result);
    }
}