export const ApiUrls = {
  baseUrl: 'http://localhost:5152/AssetIn.Server',
  Authentication: {
    SignUp: '/Authentication/UserSignUp',
    ConfirmEmail: '/Authentication/ConfirmEmail',
    SignIn: '/Authentication/SignIn',
    ForgetPassword: '/Authentication/ForgetPassword',
    ResetPassword: '/Authentication/ResetPassword',
  },
  OrganizationManagement: {
    CreateOrganization: '/OrganizationManagement/CreateOrganization',
    UpdateOrganization: '/OrganizationManagement/UpdateOrganization',
    DeleteOrganization: '/OrganizationManagement/DeleteOrganization',
    GetOrganizationInfoForOrganizationDashboard: '/OrganizationManagement/GetOrganizationInfoForOrganizationDashboard',
    GetOrganizationsListForOrganizationsDashboard: '/OrganizationManagement/GetOrganizationsListForOrganizationsDashboard'
  },
  VendorManagement: {
    GetVendorInfo: '/VendorManagement/GetVendorInfo',
    CreateUpdateVendorInfo: '/VendorManagement/CreateUpdateVendorInfo',
    CreateVendorProduct: '/VendorManagement/CreateVendorProduct',
    GetVendorProducts: '/VendorManagement/GetVendorProducts',
    UploadVendorProfilePicture: '/VendorManagement/UploadVendorProfilePicture',
  },
  AssetManagement: {
    CreateAsset: '/AssetManagement/CreateAsset',
    UpdateAsset: '/AssetManagement/UpdateAsset',
    DeleteAsset: '/AssetManagement/DeleteAsset',
    GetAllAsset: '/AssetManagement/GetAllAsset',
    GetAsset: '/AssetManagement/GetAsset',
    CreateNewAssetCatagory: '/AssetManagement/CreateNewAssetCatagory',
    DeleteAssetCatagory: '/AssetManagement/DeleteAssetCatagory',
    UpdateAssetCatagory: '/AssetManagement/UpdateAssetCatagory',
    GetAllAssetCatagory: '/AssetManagement/GetAllAssetCatagory',
    GetAllAssetStatus: '/AssetManagement/GetAllAssetStatus',
    CreateNewAssetType: '/AssetManagement/CreateNewAssetType',
    DeleteAssetType: '/AssetManagement/DeleteAssetType',
    UpdateAssetType: '/AssetManagement/UpdateAssetType',
    GetAllAssetType: '/AssetManagement/GetAllAssetType',
    GetAllAvailableAssetByCatagoryId: '/AssetManagement/GetAllAvailableAssetByCatagoryId',
  },
  EmployeeManagement: {
    GetEmployeeList: '/EmployeeManagement/GetEmployeeList',
    CreateEmployee: '/EmployeeManagement/CreateEmployee',
    LockUserAccount: '/EmployeeManagement/LockUserAccount',
    UnlockUserAccount: '/EmployeeManagement/UnlockUserAccount',
    RevokeAssetManagerPreviliges: '/EmployeeManagement/RevokeAssetManagerPreviliges',
    GrantAssetManagerPreviliges: '/EmployeeManagement/GrantAssetManagerPreviliges',
  },
  AssetRequestManagement: {
    CreateAssetRequest: '/AssetRequestManagement/CreateAssetRequest',
    GetAllAssetRequestAdminList: '/AssetRequestManagement/GetAllAssetRequestAdminList',
    GetAllAssetRequestEmployeeListStatsAndDesignatedAssets: '/AssetRequestManagement/GetAllAssetRequestEmployeeListStatsAndDesignatedAssets',
    UpdateAssetRequestStatusToAccepted: '/AssetRequestManagement/UpdateAssetRequestStatusToAccepted',
    UpdateAssetRequestStatusToDeclined: '/AssetRequestManagement/UpdateAssetRequestStatusToDeclined',
    UpdateAssetRequestStatusToFulfilled: '/AssetRequestManagement/UpdateAssetRequestStatusToFulfilled',
    UpdateAssetRequestStatusToCanceled: '/AssetRequestManagement/UpdateAssetRequestStatusToCanceled',
  },
  CrystalReporting: {
    GetFilterData: '/CrystalReporting/GetFilterData',
    GenerateHtmlReportByFilter: '/CrystalReporting/GenerateHtmlReportByFilter'
  }
};
