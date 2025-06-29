@startuml
' Class Definitions

class AssetActionsManagement {
    + RequestAsset(assetRequestDTO: AssetRequestDTO): IActionResult
    + DeclineAssetRequest(assetRequestProcessDTO: AssetRequestDeclineDTO): IActionResult
    + FulFillAssetRequest(assetRequestFulFillDTO: AssetRequestFulFillDTO): IActionResult
    + AssignAsset(assetAssignmentDTO: AssetAssignmentDTO): IActionResult
    + GetAssetRequestsByUserId(): ApiResponseDTO
    + ReturnAsset(assetReturnDTO: AssetReturnDTO): IActionResult
    + CancelRequestAsset(reqiestId: int): IActionResult
    + RetireAsset(assetRetireDTO: AssetRetireDTO): IActionResult
    + SendForMaintenance(assetMaintanenceDTO: AssetMaintanenceDTO): IActionResult
    + ReturnFromMaintenance(assetMaintanenceDTO: AssetMaintanenceDTO): IActionResult
}

class AssetCatagoryManagement {
    + GetAllAssetCategories(): ApiResponseDTO
    + GetAssetCategoryById(AssetCatagoryId: int): ApiResponseDTO
    + CreateAssetCategory(assetCatagoryDTO: AssetCatagoryDTO): IActionResult
    + UpdateAssetCategory(assetCatagoryDTO: AssetCatagoryDTO): IActionResult
    + DeleteAssetCategory(AssetCatagoryId: int): IActionResult
}

class AssetManagement {
    + CreateAsset(newAssetDTO: AssetDTO): IActionResult
    + UpdateAsset(newAssetDTO: AssetDTO): IActionResult
    + GetAllAssets(): ApiResponseDTO
    + GetAssetById(assetId: int): ApiResponseDTO
    + DeleteAsset(assetId: int): IActionResult
    + GetAvailableAssetsByCatagory(catagoryId: int): ApiResponseDTO
}

class AssetTypeManagement {
    + CreateAssetType(assetType: AssetTypeDTO): IActionResult
    + GetAllAssetTypes(): ApiResponseDTO
    + GetAssetTypeById(assetTypeId: int): ApiResponseDTO
    + UpdateAssetType(assetType: AssetTypeDTO): IActionResult
    + DeleteAssetType(assetTypeId: int): IActionResult
}

class Authentication {
    + Get(): IActionResult
    + SignUp(signUpDTO: SignUpDTO): IActionResult
    + ConfirmEmail(token: string, email: string): IActionResult
    + SignIn(signInModel: SignInDTO): IActionResult
    + ForgetPassword(forgetPasswordRequest: ForgetPasswordRequestDTO): IActionResult
    + ResetPassword(resetPasswordModel: ResetPasswordDTO): IActionResult
}

class DashboardManagement {
    + GetDashBoardStatiticsData(): ApiResponseDTO
    + GetAllPendingAssetRequests(): ApiResponseDTO
    + GetAllAssetRequests(): ApiResponseDTO
    + Search(assetQuery: string, assetCatagoriId: int): ApiResponseDTO
}

class OrganizationManagement {
    + GetOrganizationsInfo(): ApiResponseDTO
    + CreateOrganization(newOrganization: OrganizationDTO): IActionResult
    + UpdateOrganization(newOrganization: OrganizationDTO): IActionResult
    + DeleteOrganization(): IActionResult
}

class UserManagement {
    + AppointAssetManager(Appointee: UserManagementDTO): IActionResult
    + DismissAssetManager(Appointee: UserManagementDTO): IActionResult
    + DeactivateAccount(targetUser: UserManagementDTO): IActionResult
    + ActivateAccount(targetUser: UserManagementDTO): IActionResult
    + GetAllUser(): ApiResponseDTO
    + GetUserById(targetUserId: string): ApiResponseDTO
    + UpdateUserProfile(userProfileUpdateDTO: UserProfileUpdateDTO): IActionResult
    + GetMyData(): ApiResponseDTO
}

class VendorManagement {
    + CreateVendor(VendorDTO: VendorDTO): IActionResult
    + GetAllVendors(): ApiResponseDTO
    + UpdateVendor(VendorUpdate: VendorDTO): IActionResult
    + DeleteVendor(VendorId: int): IActionResult
    + GetVendorById(VendorId: int): ApiResponseDTO
}

' DTO Definitions

class AssetDTO {
    + Id: int
    + AssetName: string
    + Description: string
    + PurchaseDate: DateTime
    + PurchasePrice: double
    + SerialNumber: string
    + CreatedDate: DateTime
    + UpdatedDate: DateTime
    + AssetIdentificationNumber: string
    + Manufacturer: string
    + Model: string
    + CatagoryReleventFeildsData: string
    + OrganizationData: string
    + AssetStatusData: string
    + AssetCategoryData: string
    + AssetTypeData: string
    + VendorData: string
}

class AssetMaintanenceDTO {
    + AssetId: int
    + Description: string
}

class AssetAssignmentDTO {
    + AssignedToId: string
    + AssetId: int
    + Notes: string
}

class ApiResponseDTO {
    + Status: int
    + ResponseData: object
    + Errors: object
}

class AssetRequestDeclineDTO {
    + RequestId: int
}

class AssetRequestDTO {
    + RequestDescription: string
}

class AssetRequestFulFillDTO {
    + AssignedToId: string
    + AssetId: int
    + Notes: string
    + RequestId: int
}

class AssetRetireDTO {
    + AssetId: int
    + RetirementReason: string
}

class AssetReturnDTO {
    + AssetId: int
    + ReturnCondition: string
    + Notes: string
}

class AssetTypeDTO {
    + Id: int
    + AssetTypeName: string
    + Description: string
}

class SearchDTO {
    + searchString: string
    + Filters: List<string>
}

class AssetCatagoryDTO {
    + Id: int
    + CategoryName: string
    + Description: string
    + RelaventInputFields: string
}


class OrganizationDTO {
    + OrganizationName: string
    + Description: string
    + OrganizationDomains: List<string>
}

class ResetPasswordDTO {
    + NewPassword: string
    + ConfirmedNewPassword: string
    + Email: string
    + Token: string
}

class SignInDTO {
    + Email: string
    + Password: string
}

class SignUpDTO {
    + Email: string
    + UserName: string
    + Password: string
    + requiredRole: string
}

class UserManagementDTO {
    + Id: string
}

class UserProfileUpdateDTO {
    + UserName: string
    + ProfilePicture: IFormFile
}

class VendorDTO {
    + Id: int
    + Name: string
    + Email: string
    + OfficeAddress: string
    + PhoneNumber: string
}

class ForgetPasswordRequestDTO {
    + Email: string
}

' Relationships

AssetActionsManagement --> AssetRequestDTO
AssetActionsManagement --> AssetRequestDeclineDTO
AssetActionsManagement --> AssetRequestFulFillDTO
AssetActionsManagement --> AssetAssignmentDTO
AssetActionsManagement --> AssetReturnDTO
AssetActionsManagement --> AssetRetireDTO
AssetActionsManagement --> AssetMaintanenceDTO

AssetCatagoryManagement --> AssetCatagoryDTO

AssetManagement --> AssetDTO

AssetTypeManagement --> AssetTypeDTO

Authentication --> SignUpDTO
Authentication --> SignInDTO
Authentication --> ForgetPasswordRequestDTO
Authentication --> ResetPasswordDTO

DashboardManagement --> SearchDTO

OrganizationManagement --> OrganizationDTO

UserManagement --> UserManagementDTO
UserManagement --> UserProfileUpdateDTO

VendorManagement --> VendorDTO

@enduml
