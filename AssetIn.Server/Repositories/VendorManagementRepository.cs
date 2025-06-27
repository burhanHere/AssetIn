using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.EntityFrameworkCore;
using YourAssetManager.Server.Services;

namespace AssetIn.Server.Repositories;

public class VendorManagementRepository(ApplicationDbContext applicationDbContext, CloudinaryService cloudinaryService)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly CloudinaryService _cloudinaryService = cloudinaryService;

    public async Task<ApiResponse> GetVendorInfo(string userId)
    {
        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = targetVendor
        };
    }

    public async Task<ApiResponse> CreateUpdateVendorInfo(VendorDTO newVendorInfo, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update vendor data." }
            };
        }

        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);
        if (targetVendor != null)
        {
            targetVendor.VendorName = newVendorInfo.VendorName;
            targetVendor.OfficeAddress = newVendorInfo.OfficeAddress;
            targetVendor.PhoneNumber = newVendorInfo.PhoneNumber;
            targetVendor.Email = newVendorInfo.Email;
            targetVendor.ContactPerson = newVendorInfo.ContactPerson;
            targetVendor.Status = true;
            _applicationDbContext.Update(targetVendor);
        }
        else
        {
            Vendor newVendorInfoData = new()
            {
                VendorName = newVendorInfo.VendorName,
                OfficeAddress = newVendorInfo.OfficeAddress,
                PhoneNumber = newVendorInfo.PhoneNumber,
                Email = newVendorInfo.Email,
                ContactPerson = newVendorInfo.ContactPerson,
                ProfilePicturePath = "",
                Status = true,
                UserID = userId,
            };
            await _applicationDbContext.AddAsync(newVendorInfoData);
        }

        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new()
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Vendor data udpated successfully." }
            };
        }
        return new()
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to update vendor data." }
        };
    }

    public async Task<ApiResponse> CreateVendorProduct(VendorProductDTO newVendorProduct, string userId)
    {
        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);
        if (targetVendor == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to create vendor product." }
            };
        }

        string cloudinaryUrlOfImage = "";
        if (newVendorProduct.ProfilePicture != null)
        {
            var stream = newVendorProduct.ProfilePicture.OpenReadStream();
            cloudinaryUrlOfImage = await _cloudinaryService.UploadImageToCloudinaryAsync(stream, newVendorProduct.ProfilePicture.FileName);
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

        VendorProduct newVendorProductData = new()
        {
            ID = newVendorProduct.ID,
            ProductName = newVendorProduct.ProductName,
            Description = newVendorProduct.Description,
            Price = newVendorProduct.Price,
            Model = newVendorProduct.Model,
            ProductImage = string.IsNullOrEmpty(cloudinaryUrlOfImage) ? "" : cloudinaryUrlOfImage,
            VendorID = targetVendor.VendorID
        };
        await _applicationDbContext.AddAsync(newVendorProductData);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new()
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Vendor product created successfully." }
            };
        }
        return new()
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create vendor product." }
        };
    }

    public async Task<ApiResponse> GetVendorProducts(string userId)
    {
        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);
        // if (targetVendor == null)
        // {
        //     return new ApiResponse
        //     {
        //         Status = StatusCodes.Status403Forbidden,
        //         ResponseData = new List<string> { "Error", "Unable to fetch vendor products." }
        //     };
        // }

        var vendorProducts = await _applicationDbContext.VendorProducts
            .Where(x => x.VendorID == targetVendor.VendorID)
            .ToListAsync();

        if (vendorProducts == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "No vendor products found." }
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = vendorProducts
        };
    }

    public async Task<ApiResponse> UploadVendorProfilePicture(IFormFile file, string userId)
    {
        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);
        if (targetVendor == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to upload profile picture." }
            };
        }

        string cloudinaryUrlOfImage = "";
        if (file != null)
        {
            var stream = file.OpenReadStream();
            cloudinaryUrlOfImage = await _cloudinaryService.UploadImageToCloudinaryAsync(stream, file.FileName);
            if (string.IsNullOrEmpty(cloudinaryUrlOfImage))
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    ResponseData = new List<string>()
                    {
                        "Failed to upload profile picture."
                    }
                };
            }
        }

        targetVendor.ProfilePicturePath = cloudinaryUrlOfImage;
        _applicationDbContext.Vendors.Update(targetVendor);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new()
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Profile picture uploaded successfully." }
            };
        }
        return new()
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to upload profile picture." }
        };
    }
}