using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
public class OrganizationManagementController(ApplicationDbContext applicationDbContext, UserManager<User> userManager) : ControllerBase
{
    private readonly OrganizationManagementRepository _organizationManagementRepository = new(applicationDbContext, userManager);

    [HttpPost(template: "CreateOrganization")]
    [Authorize(Policy = "OrganizationOwnerPolicy")]
    public async Task<IActionResult> CreateOrganization([FromBody] OrganizationDto createOrganizationDTO)
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
        ApiResponse result = await _organizationManagementRepository.CreateOrganization(createOrganizationDTO, userId);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpPatch(template: "UpdateOrganization")]
    [Authorize(Policy = "OrganizationOwnerPolicy")]
    public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationDto updateOrganizationDTO)
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
        ApiResponse result = await _organizationManagementRepository.UpdateOrganization(updateOrganizationDTO, userId);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpDelete(template: "DeleteOrganization")]
    [Authorize(Policy = "OrganizationOwnerPolicy")]
    public async Task<IActionResult> DeleteOrganization(int organizationId)
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
        ApiResponse result = await _organizationManagementRepository.DeleteOrganization(organizationId, userId);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status409Conflict => Conflict(result),
            StatusCodes.Status400BadRequest => BadRequest(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpGet(template: "GetOrganizationInfoForOrganizationDashboard")]
    [Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
    public async Task<IActionResult> GetOrganizationInfoForOrganizationDashboard(int OrganizationID)
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
        ApiResponse result = await _organizationManagementRepository.GetOrganizationInfoForOrganizationDashboard(OrganizationID, userId);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }

    [HttpGet(template: "GetOrganizationsListForOrganizationsDashboard")]
    [Authorize(Policy = "OrganizationOwnerPolicy")]
    public async Task<IActionResult> GetOrganizations()
    {
        var userID = User.FindFirst("UserId")?.Value;
        if (string.IsNullOrEmpty(userID))
        {
            // If the username is not found, return an unauthorized response
            return Unauthorized(new ApiResponse
            {
                Status = StatusCodes.Status401Unauthorized,
                ResponseData = new List<string> { "User not found in token." }
            });
        }
        ApiResponse result = await _organizationManagementRepository.GetOrganizationsListForOrganizationsDashboard(userID);
        return result.Status switch
        {
            StatusCodes.Status200OK => Ok(result),
            StatusCodes.Status404NotFound => NotFound(result),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred."),
        };
    }
}