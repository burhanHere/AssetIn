using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;


[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
public class AssesmentManagementController(ApplicationDbContext applicationDbContext) : ControllerBase
{
    private readonly AssestManagementRepository _assestManagementRepository = new(applicationDbContext);
    public async Task<IActionResult> CreateAsset(AssetDTO newAsset)
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
        var result = await _assestManagementRepository.CreateAsset(newAsset, userId);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> UpdateAsset(AssetDTO newAsset)
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
        var result = await _assestManagementRepository.UpdateAsset(newAsset, userId);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> DeleteAsset(int assetID)
    {
        var result = await _assestManagementRepository.DeleteAsset(assetID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> GetAllAsset(int organizationID)
    {
        var result = await _assestManagementRepository.GetAllAsset(organizationID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> GetAsset(int assetID)
    {
        var result = await _assestManagementRepository.GetAsset(assetID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> CreateNewAssetCatagory(AssetCatagoryDTO newCatagory)
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
        var result = await _assestManagementRepository.CreateNewAssetCatagory(newCatagory, userId);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> DeleteAssetCatagory(int catagoryID)
    {
        var result = await _assestManagementRepository.DeleteAssetCatagory(catagoryID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> UpdateAssetCatagory(AssetCatagoryDTO assetCatagory)
    {
        var result = await _assestManagementRepository.UpdateAssetCatagory(assetCatagory);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> GetAllAssetCatagory(int organizationID)
    {
        var result = await _assestManagementRepository.GetAllAssetCatagory(organizationID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> GetAllAssetStatus(int organizationID)
    {
        var result = await _assestManagementRepository.GetAllAssetStatus(organizationID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        //    (result.Status == StatusCodes.Status404NotFound)
        return NotFound(result);
    }
    public async Task<IActionResult> CreateNewAssetType(AssetTypeDTO assetType)
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
        var result = await _assestManagementRepository.CreateNewAssetType(assetType, userId);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> DeleteAssetType(int AssetTypeID)
    {
        var result = await _assestManagementRepository.DeleteAssetType(AssetTypeID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> UpdateAssetType(AssetTypeDTO assetType)
    {
        var result = await _assestManagementRepository.UpdateAssetType(assetType);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
    public async Task<IActionResult> GetAllAssetType(int organizationID)
    {
        var result = await _assestManagementRepository.GetAllAssetType(organizationID);
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)

        return StatusCode(StatusCodes.Status403Forbidden, result);
    }
}