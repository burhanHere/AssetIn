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
[Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
public class EmployeeManagementController(UserManager<User> userManager, ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager) : ControllerBase
{
    private readonly EmployeeManagementRepository _employeeManagementRepository = new(userManager, applicationDbContext, roleManager);

    [HttpGet(template: "GetEmployeeList")]
    public async Task<IActionResult> GetEmployeeList(int organizationId)
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
        ApiResponse result = await _employeeManagementRepository.GetEmployeeList(userId, organizationId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "CreateEmployee")]
    public async Task<IActionResult> CreateEmployee(NewEmployeeDTO newEmployee)
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
        ApiResponse result = await _employeeManagementRepository.CreateEmployee(userId, newEmployee);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPost(template: "UpdateEmployee")]
    public async Task<IActionResult> UpdateEmployee(UpdateEmployeeDTO updatedEmployee)
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
        ApiResponse result = await _employeeManagementRepository.UpdateEmployee(userId, updatedEmployee);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "LockUserAccount")]
    public async Task<IActionResult> LockUserAccount(UpdateTargetAccountDto targetUserData)
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
        ApiResponse result = await _employeeManagementRepository.UpdateUserAccountActiveStatus(userId, targetUserData, false);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "UnlockUserAccount")]
    public async Task<IActionResult> UnlockUserAccount(UpdateTargetAccountDto targetUserData)
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
        ApiResponse result = await _employeeManagementRepository.UpdateUserAccountActiveStatus(userId, targetUserData, true);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "RevokeAssetManagerPreviliges")]
    public async Task<IActionResult> RevokeAssetManagerPreviliges(UpdateTargetAccountDto targetUserData)
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
        ApiResponse result = await _employeeManagementRepository.UpdateUserPrevliges(userId, targetUserData, 2);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpPatch(template: "GrantAssetManagerPreviliges")]
    public async Task<IActionResult> GrantAssetManagerPreviliges(UpdateTargetAccountDto targetUserData)
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

        ApiResponse result = await _employeeManagementRepository.UpdateUserPrevliges(userId, targetUserData, 3);
        return HelperFunctions.ResponseFormatter(this, result);
    }


}