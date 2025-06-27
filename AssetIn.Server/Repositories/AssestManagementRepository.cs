using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using YourAssetManager.Server.Services;

namespace AssetIn.Server.Repositories;

public class AssetManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager, CloudinaryService cloudinaryService)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;
    private readonly CloudinaryService _cloudinaryService = cloudinaryService;

    public async Task<ApiResponse> CreateAsset(AssetDTO newAsset, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to crete asset." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == newAsset.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to crete asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != newAsset.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset." }
                };
            }
        }

        string assetIdentificationNumber = "";
        while (true)
        {
            assetIdentificationNumber = Guid.NewGuid().ToString("N"); // Generate a random barcode
            if (!(await _applicationDbContext.Assets.AnyAsync(x => x.AssetIdentificationNumber == assetIdentificationNumber)))
            {
                break;
            }
        }

        string barcode = "";
        while (true)
        {
            barcode = Guid.NewGuid().ToString("N").Substring(0, 10); // Generate a random barcode
            if (!(await _applicationDbContext.Assets.AnyAsync(x => x.Barcode == barcode)))
            {
                break;
            }
        }
        string cloudinaryUrlOfImage = "";
        if (newAsset.ProfilePicturePath != null)
        {
            var stream = newAsset.ProfilePicturePath.OpenReadStream();
            cloudinaryUrlOfImage = await _cloudinaryService.UploadImageToCloudinaryAsync(stream, newAsset.ProfilePicturePath.FileName);
            if (string.IsNullOrEmpty(cloudinaryUrlOfImage))
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    ResponseData = new List<string>()
                    {
                        "Failed to update profile."
                    }
                };
            }
        }

        Asset newAssetModel = new()
        {
            AssetName = newAsset.AssetName,
            Description = newAsset.Description,
            SerialNumber = newAsset.SerialNumber,
            Barcode = barcode,
            Model = newAsset.Model,
            Manufacturer = newAsset.Manufacturer,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            PurchaseDate = newAsset.PurchaseDate,
            PurchasePrice = newAsset.PurchasePrice,
            CostPrice = newAsset.CostPrice,
            Location = newAsset.Location,
            DepreciationRate = newAsset.DepreciationRate,
            Problem = newAsset.Problem ?? "No Issue Reported.",
            AssetIdentificationNumber = assetIdentificationNumber,
            OrganizationID = requiredOrganization.OrganizationID,
            AssetStatusID = 4, // 4 = available
            AssetCatagoryID = newAsset.AssetCatagoryID,
            AssetTypeID = newAsset.AssetTypeID,
            DeletedByOrganization = false,
            ProfilePicturePath = newAsset.ProfilePicturePath != null ? cloudinaryUrlOfImage : "",
        };

        try
        {

            await _applicationDbContext.Assets.AddAsync(newAssetModel);
            var newAssetCreated = await _applicationDbContext.SaveChangesAsync();
            if (newAssetCreated > 0)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status200OK,
                    ResponseData = new List<string> { "Success", "Asset created successfully." }
                };
            }
        }
        catch (Exception ex)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to create asset. ", ex.Message }
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create asset." }
        };
    }

    public async Task<ApiResponse> UpdateAsset(AssetDTO updatedAsset, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == updatedAsset.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset." }
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
            if (validUser.OrganizationId != updatedAsset.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset." }
                };
            }
        }


        var targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == updatedAsset.AssetlD && x.OrganizationID == updatedAsset.OrganizationID);
        if (targetAsset == null)
        {
            return new ApiResponse()
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Unable to update asset." }
            };
        }

        targetAsset.AssetName = updatedAsset.AssetName;
        targetAsset.Description = updatedAsset.Description;
        targetAsset.SerialNumber = updatedAsset.SerialNumber;
        targetAsset.Model = updatedAsset.Model;
        targetAsset.Manufacturer = updatedAsset.Manufacturer;
        targetAsset.PurchaseDate = updatedAsset.PurchaseDate;
        targetAsset.Location = updatedAsset.Location;
        targetAsset.DepreciationRate = updatedAsset.DepreciationRate;
        targetAsset.Problem = updatedAsset.Problem;
        targetAsset.AssetCatagoryID = updatedAsset.AssetCatagoryID;
        targetAsset.AssetTypeID = updatedAsset.AssetTypeID;
        targetAsset.UpdatedDate = DateTime.UtcNow;

        _applicationDbContext.Assets.Update(targetAsset);
        int updateResult = await _applicationDbContext.SaveChangesAsync();
        if (updateResult > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset updated successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to updated asset." }
        };
    }
    public async Task<ApiResponse> DeleteAsset(int assetID, string userId)
    {
        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == assetID);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAsset.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to delete asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetAsset.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to delete asset." }
                };
            }
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAsset.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset." }
            };
        }
        if (targetAsset.AssetStatusID != 4)
        {
            // 1   Assigned
            // 2   Retired
            // 3   UnderMaintenance
            // 4   Available
            // 5   Lost
            // 6   Out Of Order
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is not available right now. Unable to delete." }
            };
        }
        targetAsset.DeletedByOrganization = true;

        _applicationDbContext.Assets.Update(targetAsset);
        var assetDeleted = await _applicationDbContext.SaveChangesAsync();
        if (assetDeleted > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset deleted successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to delete asset." }
        };
    }
    public async Task<ApiResponse> GetAllAsset(int organizationID, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get assets." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get assets." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != organizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get assets." }
                };
            }
        }

        var allAssets = await (from asset in _applicationDbContext.Assets
                               join status in _applicationDbContext.OrganizationsAssetStatuses
                               on asset.AssetStatusID equals status.OrganizationsAssetStatusID
                               where
                               asset.OrganizationID == organizationID &&
                               asset.DeletedByOrganization == false
                               orderby asset.CreatedDate descending
                               select new
                               {
                                   AssetlD = asset.AssetlD,
                                   AssetName = asset.AssetName,
                                   Barcode = asset.Barcode,
                                   SerialNumber = asset.SerialNumber,
                                   CreatedDate = asset.CreatedDate,
                                   UpdatedDate = asset.UpdatedDate,
                                   DeletedByOrganization = asset.DeletedByOrganization,
                                   AssetStatus = status.OrganizationsAssetStatusName,
                               }).ToListAsync();
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allAssets
        };
    }
    public async Task<ApiResponse> GetAllAvailableAssetByCatagoryId(int organizationID, int catagoryId, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get assets." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get assets." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != organizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get assets." }
                };
            }
        }

        var allAssets = await (from asset in _applicationDbContext.Assets
                               join status in _applicationDbContext.OrganizationsAssetStatuses
                               on asset.AssetStatusID equals status.OrganizationsAssetStatusID
                               where
                               asset.OrganizationID == organizationID &&
                               asset.DeletedByOrganization == false &&
                               asset.AssetCatagoryID == catagoryId &&
                               asset.AssetStatusID == 4 //4 == available
                               orderby asset.CreatedDate descending
                               select new
                               {
                                   AssetlD = asset.AssetlD,
                                   AssetName = asset.AssetName,
                                   Barcode = asset.Barcode,
                                   SerialNumber = asset.SerialNumber,
                                   CreatedDate = asset.CreatedDate,
                                   UpdatedDate = asset.UpdatedDate,
                                   DeletedByOrganization = asset.DeletedByOrganization,
                                   AssetStatus = status.OrganizationsAssetStatusName,
                               }).ToListAsync();
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allAssets
        };
    }
    public async Task<ApiResponse> GetAsset(int assetID, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset." }
            };
        }

        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == assetID && !x.DeletedByOrganization);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }

        Organization? targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAsset.OrganizationID);

        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset." }
                };
            }
        }

        //_______________________
        List<dynamic> organizationsAssetMaintenance = _applicationDbContext.OrganizationsAssetMaintanences.Where(x => targetAsset.AssetlD == x.AssetID)
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

        var organizationEmployeeIds = await _applicationDbContext.Users
            .Where(x => x.OrganizationId == targetAsset.OrganizationID || targetOrganization.UserID == x.Id)
            .Select(x => x.Id)
            .ToListAsync();

        List<dynamic> organizationsAssetAssignReturn = _applicationDbContext.OrganizationsAssetAssignReturns.Where(x => targetAsset.AssetlD == x.AssetID)
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

        List<dynamic> organizationsAssetRetirement = _applicationDbContext.OrganizationsAssetRetirements.Where(x => targetAsset.AssetlD == x.AssetID)
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
     .Concat(organizationsAssetAssignReturn)
     .Concat(organizationsAssetRetirement)
     .OrderBy(x => x.Date)];


        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new
            {
                AssetlD = targetAsset.AssetlD,
                AssetName = targetAsset.AssetName,
                Description = targetAsset.Description,
                SerialNumber = targetAsset.SerialNumber,
                Barcode = targetAsset.Barcode,
                Model = targetAsset.Model,
                Manufacturer = targetAsset.Manufacturer,
                CreatedDate = targetAsset.CreatedDate,
                UpdatedDate = targetAsset.UpdatedDate,
                PurchaseDate = targetAsset.PurchaseDate,
                PurchasePrice = targetAsset.PurchasePrice,
                CostPrice = targetAsset.CostPrice,
                DeletedByOrganization = targetAsset.DeletedByOrganization,
                Location = targetAsset.Location,
                DepreciationRate = targetAsset.DepreciationRate,
                Problem = targetAsset.Problem,
                AssetIdentificationNumber = targetAsset.AssetIdentificationNumber,
                OrganizationID = targetAsset.OrganizationID,
                AssetStatusID = targetAsset.AssetStatusID,
                AssetCatagoryID = targetAsset.AssetCatagoryID,
                AssetTypeID = targetAsset.AssetTypeID,
                ProfilePicturePath = targetAsset.ProfilePicturePath,
                RecentActivitiesList = recentActivitiesList
            }
        };
    }
    public async Task<ApiResponse> CreateNewAssetCatagory(AssetCatagoryDTO newCatagory, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to crete asset catagory." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == newCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to crete asset catagory." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to crete asset catagory." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to crete asset catagory." }
                };
            }
        }

        OrganizationsAssetCatagory newAssetCatagory = new()
        {
            OrganizationsAssetCatagoryID = newCatagory.OrganizationsAssetCatagoryID,
            OrganizationsAssetCatagoryName = newCatagory.OrganizationsAssetCatagoryName,
            OrganizationsID = targetOrganization.OrganizationID
        };

        await _applicationDbContext.OrganizationsAssetCatagories.AddAsync(newAssetCatagory);
        var newAssetCreated = await _applicationDbContext.SaveChangesAsync();
        if (newAssetCreated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset catagory created successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create asset catagory ." }
        };
    }
    public async Task<ApiResponse> DeleteAssetCatagory(int catagoryID, string userId)
    {
        OrganizationsAssetCatagory? targetCatagory = await _applicationDbContext.OrganizationsAssetCatagories.FirstOrDefaultAsync(x => x.OrganizationsAssetCatagoryID == catagoryID);
        if (targetCatagory != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Unable to delete asset catagory." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset catagory." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset catagory ." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to delete assets catagory." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized todelete assets catagory." }
                };
            }
        }

        var catagoryAssociationCheckWithAsset = await _applicationDbContext.Assets.AnyAsync(x => x.AssetCatagoryID == catagoryID);

        if (catagoryAssociationCheckWithAsset)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset catagory. Asset catagory is associated with some assets." }
            };
        }

        _applicationDbContext.OrganizationsAssetCatagories.Remove(targetCatagory!);
        var catagoryDeleted = await _applicationDbContext.SaveChangesAsync();
        if (catagoryDeleted > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset catagory deleted successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to delete asset catagory." }
        };
    }
    public async Task<ApiResponse> UpdateAssetCatagory(AssetCatagoryDTO assetCatagory, string userId)
    {
        OrganizationsAssetCatagory? targetCatagory = await _applicationDbContext.OrganizationsAssetCatagories.FirstOrDefaultAsync(x => x.OrganizationsAssetCatagoryID == assetCatagory.OrganizationsAssetCatagoryID);
        if (targetCatagory != null || !targetCatagory.Organization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset catagory." }
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset catagory ." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset catagory ." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset catagory." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset catagory." }
                };
            }
        }

        targetCatagory.OrganizationsAssetCatagoryName = assetCatagory.OrganizationsAssetCatagoryName;

        _applicationDbContext.OrganizationsAssetCatagories.Update(targetCatagory);
        var catagoryUpdated = await _applicationDbContext.SaveChangesAsync();
        if (catagoryUpdated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset catagory updated successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to update asset catagory." }
        };
    }
    public async Task<ApiResponse> GetAllAssetCatagory(int organizationID, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset catagories." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset catagories." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset catagories." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset catagories." }
                };
            }
        }

        var allCatagories = await _applicationDbContext.OrganizationsAssetCatagories
        .Where(x => x.OrganizationsID == organizationID)
        .Select(x => new OrganizationsAssetCatagory
        {
            OrganizationsAssetCatagoryID = x.OrganizationsAssetCatagoryID,
            OrganizationsAssetCatagoryName = x.OrganizationsAssetCatagoryName,
            OrganizationsID = x.OrganizationsID
        }).ToListAsync();

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allCatagories
        };
    }
    public async Task<ApiResponse> CreateNewAssetType(AssetTypeDTO assetType, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == assetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to create asset type." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to create asset type." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset type." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset type." }
                };
            }
        }

        OrganizationsAssetType newAssetType = new()
        {
            OrganizationsAssetTypeID = assetType.OrganizationsAssetTypeID,
            OrganizationsAssetTypeName = assetType.OrganizationsAssetTypeName,
            OrganizationsID = targetOrganization.OrganizationID
        };

        await _applicationDbContext.OrganizationsAssetTypes.AddAsync(newAssetType);
        var newAssetTypeCreated = await _applicationDbContext.SaveChangesAsync();
        if (newAssetTypeCreated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset type created successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create asset type." }
        };
    }
    public async Task<ApiResponse> DeleteAssetType(int AssetTypeID, string userId)
    {

        OrganizationsAssetType? targetAssetType = await _applicationDbContext.OrganizationsAssetTypes.FirstOrDefaultAsync(x => x.OrganizationsAssetTypeID == AssetTypeID);
        if (targetAssetType != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset type." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset type." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset type." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to delete asset type." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to delete asset type." }
                };
            }
        }

        var statusAssociationCheckWithAsset = await _applicationDbContext.Assets.AnyAsync(x => x.AssetTypeID == AssetTypeID);

        if (statusAssociationCheckWithAsset)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to delete asset status. Asset status is associated with some assets." }
            };
        }

        _applicationDbContext.OrganizationsAssetTypes.Remove(targetAssetType!);
        var assetTypeDeleted = await _applicationDbContext.SaveChangesAsync();
        if (assetTypeDeleted > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset type deleted successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to delete asset type." }
        };
    }
    public async Task<ApiResponse> UpdateAssetType(AssetTypeDTO assetType, string userId)
    {
        OrganizationsAssetType? targetAssetType = await _applicationDbContext.OrganizationsAssetTypes.FirstOrDefaultAsync(x => x.OrganizationsAssetTypeID == assetType.OrganizationsAssetTypeID);
        if (targetAssetType != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset type." }
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset type." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update asset type." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset type." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update asset type." }
                };
            }
        }

        targetAssetType.OrganizationsAssetTypeName = targetAssetType.OrganizationsAssetTypeName;

        _applicationDbContext.OrganizationsAssetTypes.Update(targetAssetType);
        var assetTypeUpdated = await _applicationDbContext.SaveChangesAsync();
        if (assetTypeUpdated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset type updated successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to update asset type." }
        };
    }
    public async Task<ApiResponse> GetAllAssetType(int organizationID, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset types." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset types." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset types." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset types." }
                };
            }
        }

        var allAssetType = await _applicationDbContext.OrganizationsAssetTypes
        .Where(x => x.OrganizationsID == organizationID)
        .Select(x => new
        {
            OrganizationsAssetTypeID = x.OrganizationsAssetTypeID,
            OrganizationsAssetTypeName = x.OrganizationsAssetTypeName,
            OrganizationsID = x.OrganizationsID
        })
        .ToListAsync();

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allAssetType
        };
    }
    public async Task<ApiResponse> GetAllAssetStatus(int organizationID, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset statuses." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get asset statuses." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset statuses." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get asset statuses." }
                };
            }
        }

        List<OrganizationsAssetStatus> allStatus = await _applicationDbContext.OrganizationsAssetStatuses.ToListAsync();

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allStatus
        };
    }
    public async Task<ApiResponse> SendAssetToMaintenance(SendAssetToMaintanenceDTO sendAssetToMaintanenceDTO, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == sendAssetToMaintanenceDTO.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to send asset to maintenance." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to send asset to maintenance." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to send asset to maintenance." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to send asset to maintenance." }
                };
            }
        }

        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == sendAssetToMaintanenceDTO.AssetID && !x.DeletedByOrganization);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }
        // OrganizationsAssetStatusID = 1, OrganizationsAssetStatusName = "Assigned" },
        // OrganizationsAssetStatusID = 2, OrganizationsAssetStatusName = "Retired" },
        // OrganizationsAssetStatusID = 3, OrganizationsAssetStatusName = "Under Maintenance" },
        // OrganizationsAssetStatusID = 4, OrganizationsAssetStatusName = "Available" },
        // OrganizationsAssetStatusID = 5, OrganizationsAssetStatusName = "Lost" },
        // OrganizationsAssetStatusID = 6, OrganizationsAssetStatusName = "Out Of Order" }
        if (targetAsset.AssetStatusID == 3)
        {
            // 3 = undermaintanence 
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is already under maintenance." }
            };
        }
        else if (targetAsset.AssetStatusID == 2 || targetAsset.AssetStatusID == 5 || targetAsset.AssetStatusID == 1)
        {
            // 2 = retired, 5 = lost, 1 = assigned
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is not available for maintenance." }
            };
        }

        targetAsset.AssetStatusID = 3; // 3 = undermaintanence
        OrganizationsAssetMaintanence newEntry = new()
        {
            SentDate = DateTime.Now,
            RetunDate = DateTime.MinValue,
            MaintanenceResult = "",
            Problem = sendAssetToMaintanenceDTO.Problem,
            AssetID = sendAssetToMaintanenceDTO.AssetID,
        };
        await _applicationDbContext.OrganizationsAssetMaintanences.AddAsync(newEntry);
        _applicationDbContext.Assets.Update(targetAsset);
        var result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset sent to maintenance successfully." }
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to send asset to maintenance." }
        };
    }

    public async Task<ApiResponse> ReturnAssetFromMaintenance(ReturnAssetFromMaintanenceDTO returnAssetFromMaintanenceDTO, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == returnAssetFromMaintanenceDTO.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to return asset from maintenance." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to return asset from maintenance." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to return asset from maintanence." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to return asset from maintanence." }
                };
            }
        }

        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == returnAssetFromMaintanenceDTO.AssetID && !x.DeletedByOrganization);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }
        // OrganizationsAssetStatusID = 1, OrganizationsAssetStatusName = "Assigned" },
        // OrganizationsAssetStatusID = 2, OrganizationsAssetStatusName = "Retired" },
        // OrganizationsAssetStatusID = 3, OrganizationsAssetStatusName = "Under Maintenance" },
        // OrganizationsAssetStatusID = 4, OrganizationsAssetStatusName = "Available" },
        // OrganizationsAssetStatusID = 5, OrganizationsAssetStatusName = "Lost" },
        // OrganizationsAssetStatusID = 6, OrganizationsAssetStatusName = "Out Of Order" }
        if (targetAsset.AssetStatusID != 3)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to return asset from maintenance." }
            };
        }

        OrganizationsAssetMaintanence? updateEntry = await _applicationDbContext.OrganizationsAssetMaintanences
            .FirstOrDefaultAsync(x => x.AssetID == returnAssetFromMaintanenceDTO.AssetID && x.RetunDate == DateTime.MinValue);
        if (updateEntry == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to return asset from maintenance." }
            };
        }

        if (returnAssetFromMaintanenceDTO.IsRepaired)
        {
            targetAsset.AssetStatusID = 4; // 4 = available
        }
        else
        {

            targetAsset.AssetStatusID = 6; // 6 = out of order
        }
        updateEntry.RetunDate = DateTime.Now;
        updateEntry.MaintanenceResult = returnAssetFromMaintanenceDTO.MaintanenceResult;

        _applicationDbContext.OrganizationsAssetMaintanences.Update(updateEntry);
        _applicationDbContext.Assets.Update(targetAsset);
        var result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset returned from maintenance successfully." }
            };
        }
        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to return asset from maintenance." }
        };
    }

    public async Task<ApiResponse> CheckOutAsset(CheckOutAssetDTO checkOutAssetDTO, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == checkOutAssetDTO.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to checkout asset to the user." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to checkout asset to the user." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to checkout asset to the user." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to checkout asset to the user." }
                };
            }
        }

        Asset? assetToAssign = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == checkOutAssetDTO.AssetId && !x.DeletedByOrganization);
        if (assetToAssign == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }

        if (assetToAssign.AssetStatusID != 4) // 4 = Available
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is not available for checkout." }
            };
        }


        var assignToUser = _userManager.Users.FirstOrDefault(x => x.Id == checkOutAssetDTO.AssignedToUserId);
        if (assignToUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Unable to finf the user to whom you are trying to checkout asset." }
            };
        }
        else if (!assignToUser.Status)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Cann't checkout asset to this user." }
            };
        }

        OrganizationsAssetAssignReturn newAssignReturn = new()
        {
            AssignedAt = DateTime.Now,
            ReturnedAt = DateTime.MinValue,
            Notes = checkOutAssetDTO.Notes.IsNullOrEmpty() ? "" : checkOutAssetDTO.Notes!,
            AssignedToUserID = assignToUser.Id,
            AssignedByUserID = validUser.Id,
            CheckInByUserID = null,
            CheckInNotes = "",
            AssetID = assetToAssign.AssetlD,
        };
        assetToAssign.AssetStatusID = 1; // 1 = Assigned
        await _applicationDbContext.OrganizationsAssetAssignReturns.AddAsync(newAssignReturn);
        _applicationDbContext.Assets.Update(assetToAssign);

        var result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {

            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset checked out to the user successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to checkout asset to the user." }
        };
    }

    public async Task<ApiResponse> CheckInAsset(CheckInAssetDTO checkInAssetDTO, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == checkInAssetDTO.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to checkIn asset." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to checkIn asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to checkIn asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to checkIn asset." }
                };
            }
        }

        Asset? assetToReturn = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == checkInAssetDTO.AssetId && !x.DeletedByOrganization);
        if (assetToReturn == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }

        if (assetToReturn.AssetStatusID != 1) // 1 = assigned
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to checkIn asset." }
            };
        }

        OrganizationsAssetAssignReturn? assetReturnEntry = await _applicationDbContext.OrganizationsAssetAssignReturns
            .FirstOrDefaultAsync(x => x.AssetID == checkInAssetDTO.AssetId && x.ReturnedAt == DateTime.MinValue);

        if (assetReturnEntry == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Unable to check in asset." }
            };
        }

        assetReturnEntry.ReturnedAt = DateTime.Now;
        assetReturnEntry.CheckInByUserID = validUser.Id;
        assetReturnEntry.CheckInNotes = checkInAssetDTO.Notes.IsNullOrEmpty() ? "" : checkInAssetDTO.Notes!;
        assetToReturn.AssetStatusID = 4; // 4 = Available
        _applicationDbContext.OrganizationsAssetAssignReturns.Update(assetReturnEntry);
        _applicationDbContext.Assets.Update(assetToReturn);
        var result = await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset checked in successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Some error occured while the asset CheckIn." }
        };
    }

    public async Task<ApiResponse> RetireAsset(AssetRetireDTO assetRetireDTO, string userId)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == assetRetireDTO.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to retire asset." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to retire asset." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (targetOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to retire asset." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result)
        {
            if (validUser.OrganizationId != targetOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to retire asset." }
                };
            }
        }

        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == assetRetireDTO.AssetID && !x.DeletedByOrganization);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Asset not found." }
            };
        }

        if (targetAsset.AssetStatusID == 2)
        {
            // 2 = retired
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is already retired." }
            };
        }
        else if (targetAsset.AssetStatusID != 4)
        {
            // 4 = available
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is not available for retirement." }
            };
        }

        OrganizationsAssetRetirement newRetirement = new()
        {
            RetirementReason = assetRetireDTO.RetirementReason,
            RetirementDate = assetRetireDTO.RetirementDate,
            Condition = assetRetireDTO.Condition,
            AssetID = assetRetireDTO.AssetID
        };

        targetAsset.AssetStatusID = 2; // 2 = retired
        await _applicationDbContext.OrganizationsAssetRetirements.AddAsync(newRetirement);
        _applicationDbContext.Assets.Update(targetAsset);
        var result = await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset retired successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to retire asset." }
        };
    }
}