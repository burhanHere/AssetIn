using System.Collections.ObjectModel;
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
        // domain uniqueness
        var domainIsNotUnique = await _applicationDbContext.Organizations.AnyAsync(x => x.OrganizationDomain == createOrganizationDTO.OrganizationDomain);
        if (domainIsNotUnique)
        {
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "The domain is already in use. Please choose a unique domain." }
            };
        }


        // Create the new organization
        Organization organization = new()
        {
            OrganizationName = createOrganizationDTO.OrganizationName,
            Description = createOrganizationDTO.Description,
            OrganizationDomain = createOrganizationDTO.OrganizationDomain,
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
             .FirstAsync(x => x.OrganizationID == organizationId);
        if (requiredOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<String> { "404", "Organization not found." }
            };
        }

        if (requiredOrganization.UserID != userId)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "403", "User not authorized to update this organization." },
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

    public async Task<ApiResponse> UpdateOrganization(OrganizationDto updateOrganizationDTO, string userId)
    {
        var requiredOrganizationToUpdate = await _applicationDbContext.Organizations
           .FirstAsync(x => x.OrganizationID == updateOrganizationDTO.OrganzationId);

        if (requiredOrganizationToUpdate == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "404", "Organization not found." },
            };
        }
        if (requiredOrganizationToUpdate.UserID != userId)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "403", "User not authorized to update this organization." },
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

    public async Task<ApiResponse> GetOrganizationInfoForOrganizationDashboard(int OrganizationID, string userId)
    {
        var validUser = await _userManager.FindByIdAsync(userId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "User not found." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations
                .FirstOrDefaultAsync(x => x.OrganizationID == OrganizationID);
        if (requiredOrganization == null && !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Organization not found." }
            };
        }


        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to access this organization." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to access this organization." }
                };
            }
        }

        // for organziation Employee count and pending requests count
        HashSet<string> organizationEmployeeIds = [.. _applicationDbContext.Users.Where(x => (x.OrganizationId == OrganizationID) && x.Status).Select(x => x.Id)];
        organizationEmployeeIds.Add(requiredOrganization.UserID);
        int organizationEmployeeCount = organizationEmployeeIds.Count;
        int organizationAssetPendingRequestsCount = await _applicationDbContext.OrganizationsAssetRequests.Where(
            x => organizationEmployeeIds.Contains(x.UserID) && x.RequestStatus == 2
        ).CountAsync();

        // for organziation organizationAssetWorth, organziationAssignedAssetCount, rganziationUnderMaintanenceAssetCount,organziationAssetCount,organizationAssetRatioByAssetType,chartsData,recentlyUpdatedAssetsList,recentActivitiesList
        var organizationAssetTypes = await _applicationDbContext.OrganizationsAssetTypes
            .Where(x => x.OrganizationsID == OrganizationID)
            .ToListAsync();
        var organizationAssets = await _applicationDbContext.Assets
            .Where(x => x.OrganizationID == OrganizationID && !x.DeletedByOrganization)
            .ToListAsync();
        decimal organizationAssetWorth = organizationAssets.Sum(x => x.CostPrice);
        int organziationAssignedAssetCount = organizationAssets.Count(x => x.AssetStatusID == 1);
        int organziationUnderMaintanenceAssetCount = organizationAssets.Count(x => x.AssetStatusID == 3);
        int organziationAssetCount = organizationAssets.Count;
        List<object> organizationAssetRatioByAssetType = [];
        foreach (var item in organizationAssetTypes)
        {
            organizationAssetRatioByAssetType.Add(
                new
                {
                    AssetTypeId = item.OrganizationsAssetTypeID,
                    AssetTypeName = item.OrganizationsAssetTypeName,
                    AssetRatioInType = organizationAssets.Count(x => x.AssetTypeID == item.OrganizationsAssetTypeID) / (organziationAssetCount * 1.0) * 100,
                }
            );
        }

        List<int> twoLatestYears = [.. organizationAssets.Select(x => x.PurchaseDate.Year).Distinct().OrderByDescending(x => x).Take(2)];

        Dictionary<int, List<decimal>> chartsData = twoLatestYears.ToDictionary(
                year => year,
                year => Enumerable.Range(1, 12)
                    .Select(month => organizationAssets.Where(x => x.PurchaseDate.Month == month && x.PurchaseDate.Year == year).Sum(x => x.CostPrice)
                    ).ToList()
                    );

        Dictionary<int, string> assetStatuses = await _applicationDbContext.OrganizationsAssetStatuses
            .ToDictionaryAsync(status =>
             status.OrganizationsAssetStatusID,
             status => status.OrganizationsAssetStatusName
             );

        List<object> recentlyUpdatedAssetsList = [.. organizationAssets.Take(8).Select(x => (object)new
        {
            AssetlD = x.AssetlD,
            Barcode = x.Barcode,
            UpdatedDate = x.UpdatedDate,
            assetStatus = assetStatuses[x.AssetStatusID],
        })];

        List<int> organizationAssetsIds = [.. organizationAssets.Select(x => x.AssetlD)];
        List<dynamic> organizationsAssetMaintenance = _applicationDbContext.OrganizationsAssetMaintanences.Where(x => organizationAssetsIds.Contains(x.AssetID))
        .Take(8)
        .Select(x => (object)new
        {
            Type = "Maintenance",
            Date = (x.RetunDate == DateTime.MinValue) ? x.SentDate : x.RetunDate,
            x.OrganizationsAssetMaintanenceID,
            x.Problem,
            x.MaintanenceResult,
            x.AssetID
        }).ToList();

        List<dynamic> organizationsAssetRequest = _applicationDbContext.OrganizationsAssetRequests.Where(x => organizationEmployeeIds.Contains(x.UserID))
            .Take(8)
            .Select(x => (object)new
            {
                Type = "Request",
                Date = x.RequestDate,
                x.OrganizationsAssetRequestID,
                x.Title,
                x.Description,
                x.RequestStatus,
                x.UserID
            }).ToList();

        List<dynamic> organizationsAssetAssignReturn = _applicationDbContext.OrganizationsAssetAssignReturns.Where(x => organizationAssetsIds.Contains(x.AssetID))
            .Take(8)
            .Select(x => (object)new
            {
                Type = "Assign/Return",
                Date = x.AssignedAt,
                x.ID,
                x.Notes,
                x.AssignedToUserID,
                x.AssignedByUserID,
                x.AssetID
            }).ToList();

        List<dynamic> organizationsAssetRetirement = _applicationDbContext.OrganizationsAssetRetirements.Where(x => organizationAssetsIds.Contains(x.AssetID))
            .Take(8)
            .Select(x => (object)new
            {
                Type = "Retirement",
                Date = x.RetirementDate,
                x.OrganizationsAssetRetirementID,
                x.RetirementReason,
                x.Condition,
                x.AssetID
            }).ToList();

        // Merge all the data
        List<dynamic> recentActivitiesList = [.. organizationsAssetMaintenance
     .Concat(organizationsAssetRequest)
     .Concat(organizationsAssetAssignReturn)
     .Concat(organizationsAssetRetirement)
     .OrderBy(x => x.Date)];



        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new
            {
                organizationEmployeeCount = organizationEmployeeCount,
                organizationAssetPendingRequestsCount = organizationAssetPendingRequestsCount,
                organizationAssetWorth = organizationAssetWorth,
                organziationAssignedAssetCount = organziationAssignedAssetCount,
                organziationUnderMaintanenceAssetCount = organziationUnderMaintanenceAssetCount,
                organziationAssetCount = organziationAssetCount,
                organizationAssetRatioByAssetType = organizationAssetRatioByAssetType,
                chartsData = chartsData,
                recentlyUpdatedAssetsList = recentlyUpdatedAssetsList,
                recentActivitiesList = recentActivitiesList,
            },
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
            .Where(x => x.UserID == userId && x.ActiveOrganization)
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
                OrganizationID = item.OrganizationID,
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
}