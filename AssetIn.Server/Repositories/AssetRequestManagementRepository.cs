using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class AssetRequestManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<ApiResponse> GetAllAssetRequestAdminList(int organizationId, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset requests." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset requests." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != requiredOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset." }
                };
            }
        }

        List<string> organizationEmployees = await _applicationDbContext.Users
                    .Where(x => (x.OrganizationId == organizationId) && x.Status).Select(x => x.Id)
                    .ToListAsync();
        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            organizationEmployees.Add(validUser.Id);
        }

        var requiredAssetRequests = await
        (from assetRequests in _applicationDbContext.OrganizationsAssetRequests
         join AssetRequestStatuses in _applicationDbContext.OrganizationsAssetRequestStatuses
         on assetRequests.RequestStatus equals AssetRequestStatuses.OrganizationsAssetRequestStatusID
         where assetRequests.UserID == validUser.Id
         select new
         {
             AssetRequestID = assetRequests.OrganizationsAssetRequestID,
             Title = assetRequests.Title,
             RequestDate = assetRequests.RequestDate,
             RequestStatus = AssetRequestStatuses.OrganizationsAssetRequestStatusName,
             RequestProcessedDate = assetRequests.RequestProcessedDate,
             CompletionStatus = assetRequests.CompletionStatus,
             RequestCompletedDate = assetRequests.RequestCompletedDate,
             UserID = assetRequests.UserID,
         }).ToListAsync();


        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredAssetRequests
        };
    }

    public async Task<ApiResponse> GetAllAssetRequestEmployeeList(int organizationId, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset requests." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset requests." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != requiredOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset." }
                };
            }
        }

        var requiredAssetRequests = await
        (from assetRequests in _applicationDbContext.OrganizationsAssetRequests
         join AssetRequestStatuses in _applicationDbContext.OrganizationsAssetRequestStatuses
         on assetRequests.RequestStatus equals AssetRequestStatuses.OrganizationsAssetRequestStatusID
         where assetRequests.UserID == validUser.Id
         select new
         {
             AssetRequestID = assetRequests.OrganizationsAssetRequestID,
             Title = assetRequests.Title,
             RequestDate = assetRequests.RequestDate,
             RequestStatus = AssetRequestStatuses.OrganizationsAssetRequestStatusName,
             CompletionStatus = assetRequests.CompletionStatus
         }).ToListAsync();

        //  Where(x => x.UserID == validUser.Id)
        //  Select x => new
        //  {
        //      AssetRequestID = x.OrganizationsAssetRequestID,
        //      Title = x.Title,
        //      RequestDate = x.RequestDate,
        //      RequestStatus = x.RequestStatus,
        //      CompletionStatus = x.CompletionStatus,
        //  };


        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredAssetRequests
        };
    }

    
}