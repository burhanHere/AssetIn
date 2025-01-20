export const nevbar = [
  {
    path: 'organizationsDashboard',
    acceptableRoles: ['OrganizationOwner'],
    displayName: 'Organizations Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: 'organizationAdminDashboard',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: 'organizationAssets',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets',
    iconPath: './icons/Assets.png',
  },
  {
    path: 'organizationEmployees',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Employees',
    iconPath: './icons/Employee.png',
  },
  {
    path: 'organizationAssetRequests',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets Requests',
    iconPath: './icons/AssetRequests.png',
  },
  {
    path: 'myAssetRequests',
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
