using System.Collections.Immutable;
using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class EmployeeManagementRepository(UserManager<User> userManager, ApplicationDbContext applicationDbContext, RoleManager<IdentityRole> roleManager)
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;


    public async Task<ApiResponse> GetEmployeeList(string userId, int organizationId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == organizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
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
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
                };
            }
        }

        //     var targetEmployees = await _applicationDbContext.Users.Where(x => x.OrganizationId == organizationId || x.Id == requiredOrganization.UserID).Join(_applicationDbContext.Roles.Join(
        //         _applicationDbContext.UserRoles,
        //         role => role.Id,
        //         userRole => userRole.RoleId,
        //         (role, userRole) => new
        //         {
        //             RoleName = role.Name,
        //             UserId = userRole.UserId,
        //         }
        //     ),
        //     user => user.Id,
        //     userIdRole => userIdRole.UserId,
        //     (user, userIdRole) => new
        //     {
        //         Id = user.Id,
        //         UserName = user.UserName,
        //         RoleName = userIdRole.RoleName,
        //         Email = user.Email,
        //         PhoneNumber = user.PhoneNumber,
        //         ProfilePicturePath = user.ProfilePicturePath,
        //         Gender = user.Gender,
        //         DateOfBirth = user.DateOfBirth,
        //         Status = user.Status,
        //         DateOfJoining = user.DateOfJoining,
        //         allocatedAssetIdsList = new List<Object>()
        //     }).ToListAsync();

        //     List<string> targetUserIds = targetEmployees.Select(x => x.Id).ToList();

        //     var targetUserIdAssetID = await _applicationDbContext.OrganizationsAssetAssignReturns
        //  .Where(x => targetUserIds.Contains(x.AssignedToUserID) && x.ReturnedAt == DateTime.MinValue)
        //  .GroupBy(x => x.AssignedToUserID)
        //  .ToDictionaryAsync(
        //      g => g.Key,
        //      g => g.Select(x => x.AssetID).ToList()
        //  );

        var targetEmployees = await (
            from user in _applicationDbContext.Users
            where user.OrganizationId == organizationId || user.Id == requiredOrganization.UserID
            join userRole in _applicationDbContext.UserRoles on user.Id equals userRole.UserId
            join role in _applicationDbContext.Roles on userRole.RoleId equals role.Id
            join asset in _applicationDbContext.OrganizationsAssetAssignReturns
                on user.Id equals asset.AssignedToUserID into assetGroup
            from asset in assetGroup.DefaultIfEmpty()
            where asset == null || asset.ReturnedAt == DateTime.MinValue
            group new { user, role, asset } by new
            {
                user.Id,
                user.UserName,
                role.Name,
                user.Email,
                user.PhoneNumber,
                user.ProfilePicturePath,
                user.Gender,
                user.DateOfBirth,
                user.Status,
                user.DateOfJoining
            } into g
            select new
            {
                Id = g.Key.Id,
                UserName = g.Key.UserName,
                RoleName = g.Key.Name,
                Email = g.Key.Email,
                PhoneNumber = g.Key.Name != "OrganizationOwner" ? g.Key.PhoneNumber : "",
                ProfilePicturePath = g.Key.ProfilePicturePath,
                Gender = g.Key.Gender,
                DateOfBirth = g.Key.DateOfBirth,
                Status = g.Key.Status,
                DateOfJoining = g.Key.DateOfJoining,
                allocatedAssetIdsList = g
                    .Where(x => x.asset != null && x.asset.ReturnedAt == DateTime.MinValue)
                    .Select(x => x.asset.AssetID)
                    .ToList()
            }
        ).ToListAsync();



        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = targetEmployees
        };
    }

    public async Task<ApiResponse> CreateEmployee(string userId, NewEmployeeDTO newEmployee)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == newEmployee.OrganizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
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
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
                };
            }
        }

        User newUser = new()
        {
            Email = newEmployee.userName.ToLower() + requiredOrganization.OrganizationDomain,
            UserName = newEmployee.userName,
            SecurityStamp = Guid.NewGuid().ToString(),
            Gender = newEmployee.Gender,
            DateOfBirth = newEmployee.DateOfBirth,
            PhoneNumber = newEmployee.PhoneNumber,
            OrganizationId = newEmployee.OrganizationId,
            Status = true,
            ProfilePicturePath = "",
            EmailConfirmed = true,
        };
        var createNewUser = await _userManager.CreateAsync(newUser, "Abcd1234@@@");
        if (!createNewUser.Succeeded)
        {
            //if fail to create new employee
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string>
                {"Unable to create new employee account"},
                Errors = createNewUser.Errors,
            };
        }

        // assigning the required role to the user
        var newUserRole = await _userManager.AddToRoleAsync(newUser, "OrganizationEmployee");
        if (!newUserRole.Succeeded)
        {
            // if fail to assign the required role to the user
            var deleteUser = await _userManager.DeleteAsync(newUser);
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to create new employee account." },
                Errors = null,
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string>() { "Success", "Employee Account Created Successfully" },
        };
    }


    public async Task<ApiResponse> UpdateEmployee(string userId, UpdateEmployeeDTO updatedEmployee)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == updatedEmployee.OrganizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to get employees." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
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
                    ResponseData = new List<string> { "Error", "User not authorized to get employees." }
                };
            }
        }

        var targetEmployee = await _userManager.FindByIdAsync(updatedEmployee.Id);
        if (targetEmployee == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Target Employee Not Found." }
            };
        }


        targetEmployee.Email = updatedEmployee.userName.ToLower() + requiredOrganization.OrganizationDomain;
        targetEmployee.UserName = updatedEmployee.userName;
        targetEmployee.Gender = updatedEmployee.Gender;
        targetEmployee.DateOfBirth = updatedEmployee.DateOfBirth;
        targetEmployee.PhoneNumber = updatedEmployee.PhoneNumber;

        var updateNewUser = await _userManager.UpdateAsync(targetEmployee);

        if (!updateNewUser.Succeeded)
        {
            //if fail to create new employee
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string>
                {"Unable to create new employee account"},
                Errors = updateNewUser.Errors,
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string>() { "Success", "Employee Account Update Successfully." },
        };
    }

    public async Task<ApiResponse> UpdateUserAccountActiveStatus(string currentUserId, UpdateTargetAccountDto updateAccountLockStatusDto, bool status)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to block employee's account." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == updateAccountLockStatusDto.TargetOrganizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to block employee's account." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != validUser.Id)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to block employee's account." }
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
                    ResponseData = new List<string> { "Error", "User not authorized to block employee's account." }
                };
            }
        }

        var targetUser = await _userManager.FindByIdAsync(updateAccountLockStatusDto.TargetUserId);
        if (targetUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Target User Not Found." }
            };
        }

        if (targetUser.OrganizationId != requiredOrganization.OrganizationID)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Not a valid User id." }
            };
        }

        targetUser.Status = status;

        var result = await _userManager.UpdateAsync(targetUser);

        if (!result.Succeeded)
        {
            return new()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to update target account status." },
                Errors = null,
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Success", "User Account status updated successfully." },
        };
    }

    public async Task<ApiResponse> UpdateUserPrevliges(string currentUserId, UpdateTargetAccountDto updateTargetAccountDto, int roleIdForPreviliges)
    {
        //2 for employee
        //3 for asset manager
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == currentUserId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update user previliges." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == updateTargetAccountDto.TargetOrganizationId);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to update user previliges." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != validUser.Id)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to update this user previliges." }
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
                    ResponseData = new List<string> { "Error", "User not authorized to update this user previliges." }
                };
            }
        }

        var targetUser = await _userManager.FindByIdAsync(updateTargetAccountDto.TargetUserId);
        if (targetUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status404NotFound,
                ResponseData = new List<string> { "Error", "Target User Not Found." }
            };
        }

        if (targetUser.OrganizationId != requiredOrganization.OrganizationID)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Not a valid User id." }
            };
        }

        //2 to set role employee
        //3 to set role asset manager

        string roleToRemove = roleIdForPreviliges == 2 ?
         "OrganizationAssetManager"
        : "OrganizationEmployee";

        string roleToAdd = roleIdForPreviliges == 2 ?
         "OrganizationEmployee"
        : "OrganizationAssetManager";

        // removing old previliges
        var removeRoleResult = await _userManager.RemoveFromRoleAsync(targetUser, roleToRemove);
        if (!removeRoleResult.Succeeded)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to update user previliges." }
            };
        }

        // adding new previliges
        var addRoleREsult = await _userManager.AddToRoleAsync(targetUser, roleToAdd);
        if (!addRoleREsult.Succeeded)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Unable to update user previliges." }
            };
        }

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new List<string> { "Success", "User Account status updated successfully." },
        };
    }
}