using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class VendorManagementRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<ApiResponse> GetVendorInfo(string userId)
    {
        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);

        if (targetVendor == null)
        {
            return new()
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "Data not found."
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = targetVendor
        };
    }

    public async Task<ApiResponse> CreateVendorInfo(VendorDTO newVendorInfo, string userId)
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

        var targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId);
        if (targetVendor != null)
        {
            return new()
            {
                Status = StatusCodes.Status409Conflict,
                ResponseData = "User already have a vendorship."
            };
        }

        Vendor newVendorInfoData = new()
        {
            VendorName = newVendorInfo.VendorName,
            OfficeAddress = newVendorInfo.OfficeAddress,
            PhoneNumber = newVendorInfo.PhoneNumber,
            Email = newVendorInfo.Email,
            ContactPerson = newVendorInfo.ContactPerson,
            Status = true,
            UserID = userId,
        };

        await _applicationDbContext.AddAsync(newVendorInfoData);

        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            return new()
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Vendor data saved."
            };
        }
        return new()
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to save vendor data."
        };
    }

    public async Task<ApiResponse> UpdateVendorInfo(VendorDTO updatedVendorInfo, string userId)
    {
        var currentUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (currentUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = "Unable to update vendor user data not found."
            };
        }

        Vendor? targetVendor = await _applicationDbContext.Vendors.FirstOrDefaultAsync(x => x.UserID == userId && x.VendorID == updatedVendorInfo.VendorID);
        if (targetVendor == null)
        {
            return new()
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = "Vendor Data not found."
            };
        }

        targetVendor.VendorName = updatedVendorInfo.VendorName;
        targetVendor.OfficeAddress = updatedVendorInfo.OfficeAddress;
        targetVendor.PhoneNumber = updatedVendorInfo.PhoneNumber;
        targetVendor.Email = updatedVendorInfo.Email;
        targetVendor.ContactPerson = updatedVendorInfo.ContactPerson;

        _applicationDbContext.Update(targetVendor);

        int result = await _applicationDbContext.SaveChangesAsync();

        if (result > 0)
        {
            return new()
            {
                Status = StatusCodes.Status200OK,
                ResponseData = "Vendor data updated"
            };
        }

        return new()
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = "Unable to update vendor data."
        };

    }
}