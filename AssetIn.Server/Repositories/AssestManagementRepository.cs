using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class AssetManagementRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<ApiResponse> CreateAsset(AssetDTO newAsset, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset."
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == newAsset.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset."
            };
        }

        Asset newAssetModel = new()
        {
            AssetlD = newAsset.AssetlD,
            AssetName = newAsset.AssetName,
            Description = newAsset.Description,
            SerialNumber = newAsset.SerialNumber,
            Model = newAsset.Model,
            Manufacturer = newAsset.Manufacturer,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow,
            PurchaseDate = newAsset.PurchaseDate,
            Location = newAsset.Location,
            DepreciationRate = newAsset.DepreciationRate,
            Problem = newAsset.Problem,
            AssetIdentificationNumber = newAsset.AssetIdentificationNumber,
            OrganizationID = targetOrganization.OrganizationID,
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
                ResponseData = "Asset created successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to create asset."
        };
    }
    public async Task<ApiResponse> UpdateAsset(AssetDTO updatedAsset, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to update asset."
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == updatedAsset.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to update asset."
            };
        }

        var targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == updatedAsset.AssetlD && x.OrganizationID == updatedAsset.OrganizationID);
        if (targetAsset == null)
        {
            return new ApiResponse()
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "Unable to update asset."
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
                ResponseData = "Asset updated successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to updated asset."
        };
    }
    public async Task<ApiResponse> DeleteAsset(int assetID)
    {

        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == assetID);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset found."
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAsset.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset."
            };
        }
        if (targetAsset.AssetStatusID == 4)
        {

            // 1   Assigned
            // 2   Retired
            // 3   UnderMaintenance
            // 4   Available
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = "Asset is not available right now. Unable to delete."
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
                ResponseData = "Asset deleted successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to delete asset."
        };
    }
    public async Task<ApiResponse> GetAllAsset(int organizationID)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to get all asset."
            };
        }

        List<Asset> allAssets = await _applicationDbContext.Assets.Where(x => x.OrganizationID == organizationID && !x.DeletedByOrganization).ToListAsync();
        if (allAssets.Count == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset found."
            };

        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allAssets
        };
    }
    public async Task<ApiResponse> GetAsset(int assetID)
    {
        Asset? targetAsset = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == assetID && !x.DeletedByOrganization);
        if (targetAsset == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset found."
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAsset.OrganizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to get required asset."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = targetAsset
        };
    }
    public async Task<ApiResponse> CreateNewAssetCatagory(AssetCatagoryDTO newCatagory, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset catagory ."
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == newCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset catagory ."
            };
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
                ResponseData = "Asset catagory created successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to create asset catagory ."
        };
    }
    public async Task<ApiResponse> DeleteAssetCatagory(int catagoryID)
    {
        OrganizationsAssetCatagory? targetCatagory = await _applicationDbContext.OrganizationsAssetCatagories.FirstOrDefaultAsync(x => x.OrganizationsAssetCatagoryID == catagoryID);
        if (targetCatagory != null || !targetCatagory.Organization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset catagory."
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset catagory ."
            };
        }
        var catagoryAssociationCheckWithAsset = _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetCatagoryID == catagoryID);

        if (catagoryAssociationCheckWithAsset != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset catagory. Asset catagory is associated with some assets."
            };
        }

        _applicationDbContext.OrganizationsAssetCatagories.Remove(targetCatagory!);
        var catagoryDeleted = await _applicationDbContext.SaveChangesAsync();
        if (catagoryDeleted > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Asset catagory deleted successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to delete asset catagory."
        };
    }
    public async Task<ApiResponse> UpdateAssetCatagory(AssetCatagoryDTO assetCatagory)
    {
        OrganizationsAssetCatagory? targetCatagory = await _applicationDbContext.OrganizationsAssetCatagories.FirstOrDefaultAsync(x => x.OrganizationsAssetCatagoryID == assetCatagory.OrganizationsAssetCatagoryID);
        if (targetCatagory != null || !targetCatagory.Organization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset catagory."
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetCatagory.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to update asset catagory ."
            };
        }
        targetCatagory.OrganizationsAssetCatagoryName = assetCatagory.OrganizationsAssetCatagoryName;

        _applicationDbContext.OrganizationsAssetCatagories.Update(targetCatagory);
        var catagoryUpdated = await _applicationDbContext.SaveChangesAsync();
        if (catagoryUpdated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Asset catagory deleted successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to delete asset catagory."
        };
    }
    public async Task<ApiResponse> GetAllAssetCatagory(int organizationID)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to get all asset catagory ."
            };
        }
        List<OrganizationsAssetCatagory> allCatagories = await _applicationDbContext.OrganizationsAssetCatagories.Where(x => x.OrganizationsID == organizationID).ToListAsync();
        if (allCatagories.Count == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset catagory found."
            };

        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allCatagories
        };
    }
    public async Task<ApiResponse> GetAllAssetStatus(int organizationID)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to get asset status."
            };
        }

        List<OrganizationsAssetStatus> allStatus = await _applicationDbContext.OrganizationsAssetStatuses.ToListAsync();

        if (allStatus.Count == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset status found."
            };

        }
        
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allStatus
        };
    }
    public async Task<ApiResponse> CreateNewAssetType(AssetTypeDTO assetType, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset type."
            };
        }

        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == assetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to crete asset type."
            };
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
                ResponseData = "Asset type created successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to create asset type."
        };
    }
    public async Task<ApiResponse> DeleteAssetType(int AssetTypeID)
    {
        OrganizationsAssetType? targetAssetType = await _applicationDbContext.OrganizationsAssetTypes.FirstOrDefaultAsync(x => x.OrganizationsAssetTypeID == AssetTypeID);
        if (targetAssetType != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset type."
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset type."
            };
        }

        var statusAssociationCheckWithAsset = _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetTypeID == AssetTypeID);

        if (statusAssociationCheckWithAsset != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset status. Asset status is associated with some assets."
            };
        }

        _applicationDbContext.OrganizationsAssetTypes.Remove(targetAssetType!);
        var assetTypeDeleted = await _applicationDbContext.SaveChangesAsync();
        if (assetTypeDeleted > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Asset type deleted successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to delete asset type."
        };
    }
    public async Task<ApiResponse> UpdateAssetType(AssetTypeDTO assetType)
    {
        OrganizationsAssetType? targetAssetType = await _applicationDbContext.OrganizationsAssetTypes.FirstOrDefaultAsync(x => x.OrganizationsAssetTypeID == assetType.OrganizationsAssetTypeID);
        if (targetAssetType != null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset type."
            };
        }
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetType.OrganizationsID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to update asset type."
            };
        }
        targetAssetType.OrganizationsAssetTypeName = targetAssetType.OrganizationsAssetTypeName;

        _applicationDbContext.OrganizationsAssetTypes.Update(targetAssetType);
        var assetTypeUpdated = await _applicationDbContext.SaveChangesAsync();
        if (assetTypeUpdated > 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Asset type updated successfully."
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to delete asset type."
        };
    }
    public async Task<ApiResponse> GetAllAssetType(int organizationID)
    {
        var targetOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationID);
        if (targetOrganization == null || !targetOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to delete asset type."
            };
        }

        List<OrganizationsAssetType> allAssetType = await _applicationDbContext.OrganizationsAssetTypes.Where(x => x.OrganizationsID == organizationID).ToListAsync();
        if (allAssetType.Count == 0)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "No asset type found."
            };

        }
        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = allAssetType
        };
    }
}