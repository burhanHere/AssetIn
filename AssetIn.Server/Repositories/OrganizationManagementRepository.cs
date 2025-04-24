using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Controllers;

class OrganizationManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<ApiResponse> CreateOrganization(OrganizationDto createOrganizationDTO, string userId)
    {
        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string>
                {
                    "Invalid user request.",
                    "User not found."
                }
            };
        }

        // Create the new organization
        Organization organization = new()
        {
            OrganizationName = createOrganizationDTO.OrganizationName,
            Description = createOrganizationDTO.Description,
            OrganizationLogo = "",
            CreatedDate = DateTime.UtcNow,
            ActiveOrganization = true,
            UserID = user.Id,
        };
        // Add the organization to the database
        var newAddedOrganization = await _applicationDbContext.Organizations.AddAsync(organization);
        var savedChangesResult = await _applicationDbContext.SaveChangesAsync();
        if (savedChangesResult == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string>
                {
                    "Some error Occured",
                    "Failed to create new organization."
                }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string>
            {
                "Success",
                "New organization created successfully."
            }
        };
    }

    public async Task<ApiResponse> DeleteOrganization(int organizationId, string userId)
    {
        var requiredOrganization = await _applicationDbContext.Organizations
             .FirstAsync(x => x.OrganizationID == organizationId && x.UserID == userId);
        if (requiredOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<String> { "404", "Organization not found." }
            };
        }
        requiredOrganization.ActiveOrganization = false;
        _applicationDbContext.Organizations.Update(requiredOrganization);
        var savedChanges = await _applicationDbContext.SaveChangesAsync();
        if (savedChanges == 0)
        {

            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> {
                    "Some error Occured", "Failed to delete organizatino, try again later." },
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Success", "Organizatino deleted successfully." },
        };
    }

    public async Task<ApiResponse> GetOrganizationInfoForOrganizationDashboard(int OrganizationID, string userId)
    {
        var requiredOrganization = await _applicationDbContext.Organizations
            .FirstAsync(x => x.OrganizationID == OrganizationID && x.UserID == userId);

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredOrganization,
        };
    }

    public async Task<ApiResponse> GetOrganizationsListForOrganizationsDashboard(string userId)
    {
        // Find the user by ID
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string>
                {
                    "Invalid user request.",
                    "User not found."
                }
            };
        }

        var requiredOrganizations = await _applicationDbContext.Organizations
            .Where(x => x.UserID == userId)
            .ToListAsync();
        List<object> requiresOrganizationDataList = [];
        int tempOrganizationAssetsCount = 0;
        decimal tempOrganizationAssetWorth = 0;
        int tempOrganizationEmployeeCount = 0;
        foreach (var item in requiredOrganizations)
        {
            tempOrganizationAssetsCount = await _applicationDbContext.Assets.CountAsync(x => x.OrganizationID == item.OrganizationID);
            tempOrganizationAssetWorth = await _applicationDbContext.Assets
                .Where(x => x.OrganizationID == item.OrganizationID)
                .SumAsync(x => x.CostPrice);
            tempOrganizationEmployeeCount = await _applicationDbContext.Users.CountAsync(x => x.OrganizationId == item.OrganizationID);
            requiresOrganizationDataList.Add(new
            {
                OrganizationName = item.OrganizationName,
                OrganizationLogo = item.OrganizationLogo,
                OrganizationEmployeeCount = tempOrganizationEmployeeCount,
                OrganizationAssetCount = tempOrganizationAssetsCount,
                OrganizationAssetWorth = tempOrganizationAssetWorth,
            });

        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiresOrganizationDataList,
        };
    }

    public async Task<ApiResponse> UpdateOrganization(OrganizationDto updateOrganizationDTO, string userId)
    {
        var requiredOrganizationToUpdate = await _applicationDbContext.Organizations
           .FirstAsync(x => x.OrganizationID == updateOrganizationDTO.OrganzationId && x.UserID == userId);

        if (requiredOrganizationToUpdate == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "404", "Organization not found." },
            };
        }
        requiredOrganizationToUpdate.OrganizationName = updateOrganizationDTO.OrganizationName;
        requiredOrganizationToUpdate.Description = updateOrganizationDTO.Description;
        _applicationDbContext.Organizations.Update(requiredOrganizationToUpdate);
        var savedChanges = await _applicationDbContext.SaveChangesAsync();
        if (savedChanges == 0)
        {

            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> {
                    "Some error Occured", "Failed to update organizatino, try again later." },
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Success", "Organiation updated successfully." }
        };
    }
}