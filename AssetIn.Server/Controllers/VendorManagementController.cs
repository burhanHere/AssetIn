using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
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
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        // not found
        return NotFound(result);
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
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        else if (result.Status == StatusCodes.Status409Conflict)
        {
            return Conflict(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
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
        if (result.Status == StatusCodes.Status200OK)
        {
            return Ok(result);
        }
        else if (result.Status == StatusCodes.Status400BadRequest)
        {
            return BadRequest(result);
        }
        else if (result.Status == StatusCodes.Status404NotFound)
        {
            return NotFound(result);
        }
        // (result.Status == StatusCodes.Status403Forbidden)
        return StatusCode(StatusCodes.Status403Forbidden, result);
    }

}