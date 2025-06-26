using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using Microsoft.EntityFrameworkCore;
using YourAssetManager.Server.Services;

namespace AssetIn.Server.Repositories;

class UserManagementRepository(ApplicationDbContext applicationDbContext, CloudinaryService cloudinaryService)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly CloudinaryService _cloudinaryService = cloudinaryService;

    public async Task<ApiResponse> GetUserInfo(string userId)
    {
        var targetUser = await _applicationDbContext.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

        if (targetUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "User not found." }
            };
        }
        var userRole = await _applicationDbContext.UserRoles
            .FirstOrDefaultAsync(x => x.UserId == userId);
        if (targetUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "User not found." }
            };
        }

        var role = await _applicationDbContext.Roles
            .FirstOrDefaultAsync(x => userRole.RoleId == x.Id);

        return new ApiResponse
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new
            {
                targetUser.Id,
                targetUser.UserName,
                targetUser.Email,
                targetUser.ProfilePicturePath,
                roleName = role.Name
            }
        };
    }

    public async Task<ApiResponse> UpdateUserProfilePicture(IFormFile file, string userId)
    {
        var targetUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (targetUser == null)
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

        targetUser.ProfilePicturePath = cloudinaryUrlOfImage;
        _applicationDbContext.Users.Update(targetUser);
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