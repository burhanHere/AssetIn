using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Models;
using AssetIn.Server.Services;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class AssetRequestManagementRepository(ApplicationDbContext applicationDbContext, UserManager<User> userManager, EmailService emailService)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
    private readonly UserManager<User> _userManager = userManager;
    private readonly EmailService _emailService = emailService;

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

        var requiredAssetRequests = await
        (from assetRequests in _applicationDbContext.OrganizationsAssetRequests
         join AssetRequestStatuses in _applicationDbContext.OrganizationsAssetRequestStatuses
         on assetRequests.RequestStatus equals AssetRequestStatuses.OrganizationsAssetRequestStatusID
         where assetRequests.OrganizationID == organizationId
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
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result || _userManager.IsInRoleAsync(validUser, "OrganizationEmployee").Result)
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
         where assetRequests.UserID == validUser.Id && assetRequests.OrganizationID == organizationId
         select new
         {
             AssetRequestID = assetRequests.OrganizationsAssetRequestID,
             Title = assetRequests.Title,
             RequestDate = assetRequests.RequestDate,
             RequestStatus = AssetRequestStatuses.OrganizationsAssetRequestStatusName,
             CompletionStatus = assetRequests.CompletionStatus
         }).ToListAsync();

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = requiredAssetRequests
        };
    }

    public async Task<ApiResponse> CreateAssetRequest(AssetRequestDTO assetRequestDTO, string userId)
    {

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to create asset requests." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == assetRequestDTO.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to create asset requests." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset request." }
                };
            }
        }
        else if (_userManager.IsInRoleAsync(validUser, "OrganizationAssetManager").Result || _userManager.IsInRoleAsync(validUser, "OrganizationEmployee").Result)
        {
            if (validUser.OrganizationId != requiredOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", "User not authorized to create asset request." }
                };
            }
        }

        OrganizationsAssetRequest newAssetRequest = new()
        {
            Title = assetRequestDTO.Title,
            Description = assetRequestDTO.Description,
            RequestDate = DateTime.UtcNow,
            RequestStatus = 2, // Default status for new requests is Pending whihc have id 2
            RequestProcessedDate = DateTime.MinValue,
            CompletionStatus = false,
            RequestCompletedDate = DateTime.MinValue,
            UserID = userId,
            OrganizationID = assetRequestDTO.OrganizationID,
        };

        await _applicationDbContext.OrganizationsAssetRequests.AddAsync(newAssetRequest);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {
            string subject = "Asset Request Created";
            string message = $@"
<div style=""text-align: center; margin - bottom: 20px;"">
        <h1 style=""color: #4CAF50; font-size: 24px;""> Asset Request Confirmation</h1>
    </div>

<div style= ""background-color: #ffffff; padding: 20px; border-radius: 8px;box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);"">
        <p style = ""font - size: 14px; line - height: 1.6; ""> Thank you for submitting your asset request.Below are the details of your request:<p>

        <div style = ""margin - bottom: 20px; "">
            <p style = ""font - weight: bold; ""><strong> Asset Request Title:</strong > {newAssetRequest.Title}</p>
            <p style = ""font - weight: bold; "" ><strong> Description:</strong > {newAssetRequest.Description}</p>
            <p style = ""font - weight: bold; "" ><strong> Request Date:</strong > {newAssetRequest.RequestDate}</p>
        </div>
 </div> ";
            bool emailResult = await _emailService.SendEmailAsync(validUser.Email!, subject, message);
            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", "Asset request created successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", "Unable to create asset request." }
        };
    }

    public async Task<ApiResponse> UpdateAssetRequestStatus(int assetRequestId, int statusId, string userId)
    {
        string requiredWord = statusId == 1 ? " Accepted" :
                              statusId == 3 ? " Declined" :
                              statusId == 4 ? " Fillfilled" :
                              /*statusId == 5 ?*/ " Canceled";
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", $"Unable to update asset requests status to {requiredWord}." }
            };
        }

        var targetAssetRequest = await _applicationDbContext.OrganizationsAssetRequests.FirstOrDefaultAsync(x => x.OrganizationsAssetRequestID == assetRequestId);
        if (targetAssetRequest == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", $"Unable to update asset requests status to {requiredWord}." }
            };
        }

        if (targetAssetRequest.RequestStatus == 3 || targetAssetRequest.RequestStatus == 5)// 3 = declined & 5 = canceled
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Cant update status request already" + (targetAssetRequest.RequestStatus == 5 ? "Canceled" : "Declined") + "." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetRequest.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", $"Unable to update asset requests status to {requiredWord}." }
            };
        }

        if (_userManager.IsInRoleAsync(validUser, "OrganizationOwner").Result)
        {
            if (requiredOrganization.UserID != userId)
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
            if (validUser.OrganizationId != requiredOrganization.OrganizationID)
            {
                return new ApiResponse
                {
                    Status = StatusCodes.Status403Forbidden,
                    ResponseData = new List<string> { "Error", $"User not authorized to update asset request status to to {requiredWord}." }
                };
            }
        }

        if (targetAssetRequest.RequestStatus == statusId)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status208AlreadyReported,
                ResponseData = new List<string> { "Success", string.Format("The assert request is already{0}.", requiredWord) }
            };
        }

        string messageText = "";

        if (statusId == 1 || // Accepted
            statusId == 3 || // Declined
            statusId == 4) // Fulfilled
                           //2 == Pending
        {
            //these actions can only be performed by organization owner and asset meneger
            if (statusId == 1 && targetAssetRequest.RequestStatus == 2)
            {
                // asset request can be accepted only if current status is Pending
                messageText = "Your asset request has been accepted.";
                targetAssetRequest.RequestProcessedDate = DateTime.UtcNow;
            }
            else if (statusId == 3 && targetAssetRequest.RequestStatus == 2)
            {
                // asset request can be declined only if current status is Pending
                messageText = "Your asset request has been declined.";
                targetAssetRequest.RequestProcessedDate = DateTime.UtcNow;
            }
            else if (statusId == 4 && targetAssetRequest.RequestStatus == 1)
            {
                // asset request can be fullfiled only if current status is Accepted
                messageText = "Your asset request has been fulfilled.";
                targetAssetRequest.RequestCompletedDate = DateTime.UtcNow;
                targetAssetRequest.CompletionStatus = true;
            }
            else
            {
                // else cant perform the action 
                return new ApiResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    ResponseData = new List<string> { "Error", "Unable to update asset request status to {requiredWord}." }
                };
            }
        }
        else // Cancled
        {
            if (statusId == 5 && targetAssetRequest.RequestStatus != 4)
            {
                //this action can only be performed by an employee
                // asset request can be cancil until current status is not updated to fullfiled
                messageText = "Your asset request has been Canceld.\n";
            }
            else
            {
                // else cant perform the action 
                return new ApiResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    ResponseData = new List<string> { "Error", "Unable to update asset request status to {requiredWord}." }
                };
            }
        }

        //updating status
        targetAssetRequest.RequestStatus = statusId;


        messageText += " Below are the details of your request:";

        _applicationDbContext.OrganizationsAssetRequests.Update(targetAssetRequest);
        int result = await _applicationDbContext.SaveChangesAsync();
        if (result > 0)
        {

            string subject = "Asset Request Update";
            string messageTemplate = $@"
<div style=""text-align: center;
            margin-bottom: 20px;"">
	<h1 style=""color: #4CAF50;
            font-size: 24px;"">Asset Request Update</h1>
</div>

<div style=""background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);"">
        <p style=""font-size: 14px;
            line-height: 1.6;"">{messageText}</p>

        <div style=""margin-bottom: 20px;"">
            <p style=""font-weight: bold;""><strong>Asset Request Title:</strong> {targetAssetRequest.Title}</p>
            <p style=""font-weight: bold;""><strong>Description:</strong> {targetAssetRequest.Description}</p>
            <p style=""font-weight: bold;""><strong>Request Date:</strong> {targetAssetRequest.RequestDate}</p>
        </div>
 </div>";
            bool emailResult = await _emailService.SendEmailAsync(validUser.Email!, subject, messageTemplate);

            return new ApiResponse
            {
                Status = StatusCodes.Status200OK,
                ResponseData = new List<string> { "Success", $"Asset request status updated to {requiredWord} successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", $"Unable to update asset request status to {requiredWord}." }
        };
    }
}