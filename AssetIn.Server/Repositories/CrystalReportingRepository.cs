using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace AssetIn.Server.Repositories;

public class CrystalReportingRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;


    public async Task<List<Asset>> GetAssetsAsync()
    {
        List<Asset> assets = await _applicationDbContext.Assets
            .Where(x => !x.DeletedByOrganization)
            .ToListAsync();
        return assets;
    }

    public async Task<ApiResponse> GetFilterData(int organizationId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (targetOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Organization not found. Can't get filters data." }
            };
        }
        var organizationAssetTypes = await _applicationDbContext.OrganizationsAssetTypes
            .Where(x => x.OrganizationsID == organizationId)
            .Select(x => new
            {
                Id = x.OrganizationsAssetTypeID,
                Name = x.OrganizationsAssetTypeName
            })
            .ToListAsync();

        var organizationAssetStatuses = await _applicationDbContext.OrganizationsAssetStatuses
            .Select(x => new
            {
                Id = x.OrganizationsAssetStatusID,
                Name = x.OrganizationsAssetStatusName
            })
            .ToListAsync();

        var organizationAssetCategories = await _applicationDbContext.OrganizationsAssetCatagories
        .Where(x => x.OrganizationsID == organizationId)
        .Select(x => new
        {
            Id = x.OrganizationsAssetCatagoryID,
            Name = x.OrganizationsAssetCatagoryName
        })
        .ToListAsync();

        var employees = await _applicationDbContext.Users
            .Where(x => x.OrganizationId == organizationId || targetOrganization.UserID == x.Id)
            .Select(x => new
            {
                Id = x.Id,
                Name = x.Email
            })
            .ToListAsync();

        var employeeStatus = new List<object>
        {
            new { Id = true, Name = "Active" },
            new { Id = false, Name = "Inactive" }
        };

        var organizationOwner = await _applicationDbContext.Users
        .Where(x => x.Id == targetOrganization.UserID).Select(x => new { Id = x.Id, Name = x.Email }).ToListAsync();
        employees.AddRange(organizationOwner);

        var employeeRoles = await _applicationDbContext.Roles
            .Where(x => x.Name != "Vendor")
            .Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();



        var gender = await _applicationDbContext.Users
        .Where(x => x.OrganizationId == organizationId)
        .Select(x => x.Gender).Distinct()
            .ToListAsync();

        var assetRequestStatus = await _applicationDbContext.OrganizationsAssetRequestStatuses
            .Select(x => new
            {
                Id = x.OrganizationsAssetRequestStatusID,
                Name = x.OrganizationsAssetRequestStatusName
            })
            .ToListAsync();

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new
            {
                assetRequestStatus = assetRequestStatus,
                gender = gender,
                employeeRoles = employeeRoles,
                employees = employees,
                employeeStatus = employeeStatus,
                organizationAssetCategories = organizationAssetCategories,
                organizationAssetStatuses = organizationAssetStatuses,
                organizationAssetTypes = organizationAssetTypes
            }
        };
    }

    public async Task<ApiResponse> GetAssetsReportDataAsync(int assetType, int assetStatus, int assetCategory, string? assignedTo, DateTime? toDate, DateTime? fromDate, int organizationId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (targetOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Organization not found. Can't create report." }
            };
        }

        IQueryable<Asset> query = _applicationDbContext.Assets
            .Where(x => !x.DeletedByOrganization);

        if (assetType != default)
        {
            query = query.Where(x => x.AssetTypeID == assetType);
        }

        if (assetStatus != default)
        {
            query = query.Where(x => x.AssetStatusID == assetStatus);
        }

        if (assetCategory != default)
        {
            query = query.Where(x => x.AssetCatagoryID == assetCategory);
        }

        if (assignedTo != default)
        {
            var validUser = _applicationDbContext.Users
                .Where(x => x.Id == assignedTo)
                .FirstOrDefault();
            if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
            {
                if (targetOrganization.UserID != validUser.Id)
                {
                    return new ApiResponse
                    {
                        Status = StatusCodes.Status403Forbidden,
                        ResponseData = new List<string> { "Error", $"User not authorized to update asset." }
                    };
                }
            }
            else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result || _userManager.IsInRoleAsync(validUser, "OrganizationEmployee").Result)
            {
                if (validUser.OrganizationId != targetOrganization.OrganizationID)
                {
                    return new ApiResponse
                    {
                        Status = StatusCodes.Status403Forbidden,
                        ResponseData = new List<string> { "Error", $"User not authorized to fulfill this asset request." }
                    };
                }
            }
            var assetIdFromRequestFromTheUser = await _applicationDbContext.OrganizationsAssetAssignReturns.Where(x => x.AssignedToUserID == assignedTo && x.ReturnedAt == DateTime.MinValue).Select(x => x.AssetID).ToListAsync();
            query = query.Where(x => assetIdFromRequestFromTheUser.Contains(x.AssetlD));
        }

        if (fromDate != default)
        {
            query = query.Where(x => x.UpdatedDate >= fromDate);
        }

        if (toDate != default)
        {
            query = query.Where(x => x.UpdatedDate <= toDate);
        }

        var assetTypes = await _applicationDbContext.OrganizationsAssetTypes
          .Where(x => x.OrganizationsID == organizationId)
          .Select(x => new
          {
              Id = x.OrganizationsAssetTypeID,
              Name = x.OrganizationsAssetTypeName
          })
          .ToListAsync();
        var assetStatuses = await _applicationDbContext.OrganizationsAssetStatuses
            .Select(x => new
            {
                Id = x.OrganizationsAssetStatusID,
                Name = x.OrganizationsAssetStatusName
            })
            .ToListAsync();
        var assetCategories = await _applicationDbContext.OrganizationsAssetCatagories
            .Where(x => x.OrganizationsID == organizationId)
            .Select(x => new
            {
                Id = x.OrganizationsAssetCatagoryID,
                Name = x.OrganizationsAssetCatagoryName
            })
            .ToListAsync();

        if (query.Count() == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "No asset found for the given criteria." }
            };
        }

        var assets = await query.Select(x => new
        {
            x.AssetCatagoryID,
            x.AssetIdentificationNumber,
            x.AssetName,
            x.AssetStatusID,
            x.AssetTypeID,
            x.AssetlD,
            x.Barcode,
            x.CostPrice,
            x.CreatedDate,
            x.Description,
            x.Location,
            x.Manufacturer,
            x.Model,
            x.Problem,
            x.ProfilePicturePath,
            x.PurchaseDate,
            x.PurchasePrice,
            x.SerialNumber,
            x.UpdatedDate
        }).ToListAsync();

        var updatedAsset = assets.Select(x => new
        {
            x.AssetlD,
            x.AssetIdentificationNumber,
            x.AssetName,
            x.Description,
            x.Location,
            x.SerialNumber,
            x.Manufacturer,
            x.Model,
            x.PurchaseDate,
            x.PurchasePrice,
            x.CostPrice,
            x.Barcode,
            x.CreatedDate,
            x.UpdatedDate,
            AssetTypeName = assetTypes.FirstOrDefault(a => a.Id == x.AssetTypeID)?.Name,
            AssetCategoryName = assetCategories.FirstOrDefault(c => c.Id == x.AssetCatagoryID)?.Name,
            AssetStatusName = assetStatuses.FirstOrDefault(s => s.Id == x.AssetStatusID)?.Name,
        }).ToList();

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = updatedAsset
        };
    }

    public async Task<ApiResponse> GetEmployeeReportDataAsync(string? employeeRole, bool employeeStatus, string? specificEmployee, string? gender, int organizationId, DateTime? toDate, DateTime? fromDate)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (targetOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Organization not found. Can't create report." }
            };
        }

        IQueryable<User> query = _applicationDbContext.Users
            .Where(x => x.OrganizationId == organizationId || x.Id == targetOrganization.UserID);

        if (employeeRole != null)
        {
            var role = await _applicationDbContext.Roles.FirstOrDefaultAsync(x => x.Id == employeeRole);
            // query = query.Where(x => _userManager.IsInRoleAsync(x, role.Name).Result);
            query = query.Where(user => _applicationDbContext.UserRoles
          .Any(ur => ur.UserId == user.Id && ur.RoleId == role.Id));
        }

        if (specificEmployee != null)
        {
            query = query.Where(x => x.Id == specificEmployee);
        }


        query = query.Where(x => x.Status == employeeStatus);


        if (gender != null)
        {
            query = query.Where(x => x.Gender == gender);
        }

        if (fromDate != null)
        {
            query = query.Where(x => x.DateOfJoining >= fromDate);
        }

        if (toDate != null)
        {
            query = query.Where(x => x.DateOfJoining <= toDate);
        }

        // Use Any() instead of Count() for better performance
        if (!await query.AnyAsync())
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "No employees found for the given criteria." }
            };
        }

        // Step 1: Project in a valid way for EF (no dictionary here)
        var employeeList = await query.Select(x => new
        {
            x.Id,
            x.Email,
            x.UserName,
            x.PhoneNumber,
            x.ProfilePicturePath,
            AccountStatus = x.Status ? "Active" : "Inactive",
            x.Gender,
            x.DateOfJoining,
            x.DateOfBirth
        }).ToListAsync();

        // Step 2: Now in memory, convert to dictionary with your custom column names
        var employees = employeeList.Select(x => new Dictionary<string, object?>
        {
            ["Id"] = x.Id,
            ["Email"] = x.Email,
            ["User Name"] = x.UserName,
            ["Phone Number"] = x.PhoneNumber,
            ["Profile Picture Path"] = x.ProfilePicturePath,
            ["Account Status"] = x.AccountStatus,
            ["Gender"] = x.Gender,
            ["Date Of Joining"] = x.DateOfJoining,
            ["Date Of Birth"] = x.DateOfBirth
        }).ToList();
        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = employees
        };
    }
    public async Task<ApiResponse> GetAssetsRequestsReportDataAsync(int requestStatus, string? requestedBy, DateTime? toDate, DateTime? fromDate, int organizationId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (targetOrganization == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Organization not found. Can't create report." }
            };
        }
        var employeeIdList = await _applicationDbContext.Users
                    .Where(x => x.OrganizationId == organizationId || x.Id == targetOrganization.UserID).Select(x => x.Id).ToListAsync();

        IQueryable<OrganizationsAssetRequest> query = _applicationDbContext.OrganizationsAssetRequests
            .Where(x => employeeIdList.Contains(x.UserID));

        if (requestStatus != default)
        {
            query = query.Where(x => x.RequestStatus == requestStatus);
        }

        if (requestedBy != default)
        {
            query = query.Where(x => x.UserID == requestedBy);
        }

        if (fromDate != default)
        {
            query = query.Where(x => x.RequestDate >= fromDate);
        }

        if (toDate != default)
        {
            query = query.Where(x => x.RequestDate <= toDate);
        }

        var assetRequestStatuses = await _applicationDbContext.OrganizationsAssetRequestStatuses
            .Select(x => new
            {
                Id = x.OrganizationsAssetRequestStatusID,
                Name = x.OrganizationsAssetRequestStatusName
            })
            .ToListAsync();
        if (query.Count() == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "No asset request found for the given criteria." }
            };
        }
        var rawData = await query
        .Select(x => new
        {
            x.OrganizationsAssetRequestID,
            x.Title,
            x.RequestDate,
            x.RequestProcessedDate,
            x.RequestStatus,
            x.CompletionStatus,
            x.RequestCompletedDate,
            x.UserID,
            x.AssetAssignmentId
        })
        .ToListAsync();

        // Now map `RequestStatus` using the in-memory list
        var assetRequests = rawData.Select(x => new Dictionary<string, object?>
        {
            ["Id"] = x.OrganizationsAssetRequestID,
            ["Title"] = x.Title,
            ["RequestDate"] = x.RequestDate,
            ["RequestProcessedDate"] = x.RequestProcessedDate,
            ["RequestStatus"] = assetRequestStatuses.FirstOrDefault(y => y.Id == x.RequestStatus)?.Name ?? "Unknown",
            ["CompletionStatus"] = x.CompletionStatus ? "Completeed" : "-",
            ["RequestCompletedDate"] = x.RequestCompletedDate,
            ["UserId"] = x.UserID,
            ["AssetAssignmentId"] = x.AssetAssignmentId
        }).ToList();


        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = assetRequests
        };
    }
}