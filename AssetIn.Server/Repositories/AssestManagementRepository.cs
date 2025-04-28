using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class AssetManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;

    public async Task<ApiResponse> CreateAsset(AssetDTO newAsset, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        Asset newAssetModel = new()
        {
            AssetName = newAsset.AssetName,
            Description = newAsset.Description,
            SerialNumber = newAsset.SerialNumber,
            Barcode = newAsset.Barcode,
            Model = newAsset.Model,
            Manufacturer = newAsset.Manufacturer,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            PurchaseDate = newAsset.PurchaseDate,
            Location = newAsset.Location,
            DepreciationRate = newAsset.DepreciationRate,
            Problem = newAsset.Problem,
            AssetIdentificationNumber = newAsset.AssetIdentificationNumber,
            OrganizationID = requiredOrganization.OrganizationID,
            AssetStatusID = newAsset.AssetStatusID,
            AssetCatagoryID = newAsset.AssetCatagoryID,
            AssetTypeID = newAsset.AssetTypeID,
            DeletedByOrganization = false,
        };

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

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create asset." }
        };
    }
    public async Task<ApiResponse> UpdateAsset(AssetDTO updatedAsset, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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
        targetAsset.Barcode = updatedAsset.Barcode;
        targetAsset.Model = updatedAsset.Model;
        targetAsset.Manufacturer = updatedAsset.Manufacturer;
        targetAsset.PurchaseDate = updatedAsset.PurchaseDate;
        targetAsset.Location = updatedAsset.Location;
        targetAsset.DepreciationRate = updatedAsset.DepreciationRate;
        targetAsset.Problem = updatedAsset.Problem;
        targetAsset.AssetIdentificationNumber = updatedAsset.AssetIdentificationNumber;
        targetAsset.AssetStatusID = updatedAsset.AssetStatusID;
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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
    public async Task<ApiResponse> GetAsset(int assetID, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = targetAsset
        };
    }
    public async Task<ApiResponse> CreateNewAssetCatagory(AssetCatagoryDTO newCatagory, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        List<OrganizationsAssetCatagory> allCatagories = await _applicationDbContext.OrganizationsAssetCatagories.Where(x => x.OrganizationsID == organizationID).ToListAsync();

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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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

        List<OrganizationsAssetType> allAssetType = await _applicationDbContext.OrganizationsAssetTypes.Where(x => x.OrganizationsID == organizationID).ToListAsync();

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

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
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
}