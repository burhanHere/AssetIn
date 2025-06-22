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
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
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
             Description = assetRequests.Description,
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

    public async Task<ApiResponse> GetAllAssetRequestEmployeeListStatsAndDesignatedAssets(int organizationId, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
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
             Description = assetRequests.Description,
             RequestDate = assetRequests.RequestDate,
             RequestStatus = AssetRequestStatuses.OrganizationsAssetRequestStatusName,
             CompletionStatus = assetRequests.CompletionStatus
         }).ToListAsync();
        /*'1', 'Accepted'
        '2', 'Pending'
        '3', 'Declined'
        '4', 'Fulfilled'
        '5', 'Canceled'
        */
        int totalRequests = requiredAssetRequests.Count;
        int pendingRequests = requiredAssetRequests.Count(x => x.RequestStatus == "Pending");
        int acceptedRequests = requiredAssetRequests.Count(x => x.RequestStatus == "Accepted");
        int declinedRequests = requiredAssetRequests.Count(x => x.RequestStatus == "Declined");
        int fulfilledRequests = requiredAssetRequests.Count(x => x.RequestStatus == "Fulfilled");
        int canceledRequests = requiredAssetRequests.Count(x => x.RequestStatus == "Canceled");

        var designatedAssetsAssetdetails = await (
        from assign in _applicationDbContext.OrganizationsAssetAssignReturns
        join asset in _applicationDbContext.Assets
            on assign.AssetID equals asset.AssetlD
        where assign.AssignedToUserID == validUser.Id && asset.AssetStatusID == 1
        select new
        {
            AssetID = asset.AssetlD,
            ProfilePicturePath = asset.ProfilePicturePath,
            AssetName = asset.AssetName,
            SerialNumber = asset.SerialNumber,
        }
        ).ToListAsync();

        return new()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = new
            {
                requiredAssetRequests = requiredAssetRequests,
                designatedAssetsAssetdetails = designatedAssetsAssetdetails,
                totalRequests = totalRequests,
                pendingRequests = pendingRequests,
                acceptedRequests = acceptedRequests,
                declinedRequests = declinedRequests,
                fulfilledRequests = fulfilledRequests,
                canceledRequests = canceledRequests,
            }
        };
    }

    public async Task<ApiResponse> CreateAssetRequest(AssetRequestDTO assetRequestDTO, string userId)
    {

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
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
                              statusId == 4 ? " Fulfilled" :
                              /*statusId == 5 ?*/ " Canceled";
        if (requiredWord == "Fulfilled")
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", $"Invalid Operation." }
            };
        }

        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
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
            statusId == 3) // Declined
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
            else if (statusId == 3 && targetAssetRequest.RequestStatus == 1)
            {
                // asset request can be declined only if current status is Pending
                messageText = "Your asset request has been declined.";
                targetAssetRequest.RequestProcessedDate = DateTime.UtcNow;
            }
            else
            {
                // else cant perform the action 
                return new ApiResponse
                {
                    Status = StatusCodes.Status400BadRequest,
                    ResponseData = new List<string> { "Error", $"Unable to update asset request status to {requiredWord}." }
                };
            }
        }
        else // Cancled
        {
            if (statusId == 5 && targetAssetRequest.RequestStatus == 2)
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

    public async Task<ApiResponse> FulFillAssetRequest(FulfillAssetRequestDTO fullfilAssetRequestDTO, string userId)
    {
        var validUser = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == userId && x.Status);
        if (validUser == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to fullfill asset request." }
            };
        }

        var targetAssetRequest = await _applicationDbContext.OrganizationsAssetRequests.FirstOrDefaultAsync(x => x.OrganizationsAssetRequestID == fullfilAssetRequestDTO.AssetRequestId);
        if (targetAssetRequest == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to fullfill asset request." }
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
        else if (targetAssetRequest.RequestStatus == 4)// 4 = fulfiled 
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status208AlreadyReported,
                ResponseData = new List<string> { "Error", "Asset request already fulfiled." }
            };
        }

        Organization? requiredOrganization = await _applicationDbContext.Organizations.FirstOrDefaultAsync(x => x.OrganizationID == targetAssetRequest.OrganizationID);
        if (requiredOrganization == null || !requiredOrganization.ActiveOrganization)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to fullfill asset request." }
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
                    ResponseData = new List<string> { "Error", $"User not authorized to fulfill this asset request." }
                };
            }
        }

        var assetToAssign = await _applicationDbContext.Assets.FirstOrDefaultAsync(x => x.AssetlD == fullfilAssetRequestDTO.AssetID);
        if (assetToAssign == null)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status403Forbidden,
                ResponseData = new List<string> { "Error", "Unable to fullfill asset request." }
            };
        }
        if (assetToAssign.AssetStatusID != 4) // 4 = Available
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Asset is not available for assignment." }
            };
        }

        if (!targetAssetRequest.User.Status)
        {
            return new ApiResponse
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "The requisitioner is not allowed to get assets. Requisitioner account is locked." }
            };
        }

        OrganizationsAssetAssignReturn newAssetAssign = new()
        {
            AssignedAt = DateTime.Now,
            ReturnedAt = DateTime.MinValue,
            Notes = fullfilAssetRequestDTO.Notes,
            AssignedToUserID = targetAssetRequest.UserID,
            AssignedByUserID = validUser.Id,
            AssetID = assetToAssign.AssetlD,
        };
        var assignmentResult = await _applicationDbContext.OrganizationsAssetAssignReturns.AddAsync(newAssetAssign);

        targetAssetRequest.RequestStatus = 4; // 4 = Fulfilled
        targetAssetRequest.CompletionStatus = true;
        targetAssetRequest.RequestCompletedDate = DateTime.Now;
        targetAssetRequest.AssetAssignmentId = assignmentResult.Entity.ID;

        assetToAssign.AssetStatusID = 1; // 1 = Assigned

        _applicationDbContext.OrganizationsAssetRequests.Update(targetAssetRequest);
        _applicationDbContext.Assets.Update(assetToAssign);

        // add asset assign logic here 
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
            line-height: 1.6;"">Your asset request has been successfully fulfilled. Please find the details below.</p>

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
                ResponseData = new List<string> { "Success", $"Asset request fulFilled successfully." }
            };
        }

        return new ApiResponse
        {
            Status = StatusCodes.Status400BadRequest,
            ResponseData = new List<string> { "Error", $"Unable to fulfill asset request." }
        };
    }


}