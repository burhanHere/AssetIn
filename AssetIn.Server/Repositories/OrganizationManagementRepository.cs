using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Controllers;

class OrganizationManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
{
    private ApplicationDbContext _applicationDbContext = applicationDbContext;
    private UserManager<User> _userManager = userManager;

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
                "Failed to create new organization."
                }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string>
            {
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
                ResponseData = new List<String> { "Organization not found." }
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
                ResponseData = new List<string> { "Failed to delete organizatino, try again later." },
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Organizatino deleted successfully." },
        };
    }

    public async Task<ApiResponse> GetOrganizationInfo(int OrganizationID,string userId)
    {
        var requiredOrganization = await _applicationDbContext.Organizations
            .FirstAsync(x => x.OrganizationID == OrganizationID && x.UserID == userId);

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredOrganization,
        };
    }

    public async Task<ApiResponse> GetOrganizations(string userId)
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

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredOrganizations,
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
                ResponseData = new List<string> { "Organization not found." },
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
                ResponseData = new List<string> { "Failed to update organizatino, try again later." },
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Organiation updated successfully." }
        };
    }
}