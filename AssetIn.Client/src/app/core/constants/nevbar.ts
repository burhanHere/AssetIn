export const nevbar = [
  {
    path: 'organizationsDashboard',
    acceptableRoles: ['OrganizationOwner'],
    displayName: 'Organizations Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: '../../organizationAdmin/organizationAdminMain/organizationAdminDashboard',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: '../../organizationAdmin/organizationAdminMain/organizationAssets',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets',
    iconPath: './icons/Assets.png',
  },
  {
    path: '../../organizationAdmin/organizationAdminMain/organizationEmployees',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Employees',
    iconPath: './icons/Employee.png',
  },
  {
    path: '../../organizationAdmin/organizationAdminMain/organizationAssetRequests',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets Requests',
    iconPath: './icons/AssetRequests.png',
  },
  {
    path: '../../organizationEmployee/organizationEmployeeMain/myAssetRequests',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager', 'OrganizationEmployee'],
    displayName: 'My Assets Requests',
    iconPath: './icons/AssetRequests.png',
  },
  {
    path: 'vendorDashboard',
    acceptableRoles: ['Vendor'],
    displayName: 'Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: 'organizationVendorChatBox',
    acceptableRoles: ['OrganizationOwner', 'Vendor'],
    displayName: 'OV-Chat',
    iconPath: './icons/OVChat.png',
  },
  {
    path: 'settings',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager', 'OrganizationEmployee', 'Vendor'],
    displayName: 'Settings',
    iconPath: './icons/Settings.png',
  },
];
