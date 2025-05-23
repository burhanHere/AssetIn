export const nevbar = [
  {
    path: '../../board/mainBoard/organizationOwner/organizationsDashboard',
    acceptableRoles: ['OrganizationOwner'],
    displayName: 'Organizations Dashboard',
    iconPath: './icons/Organizations.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationDashboard',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationAssets',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets',
    iconPath: './icons/Assets.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationEmployees',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Employees',
    iconPath: './icons/Employee.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationAssetsRequet',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager'],
    displayName: 'Assets Requests',
    iconPath: './icons/AssetRequests.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationVendors',
    acceptableRoles: ['OrganizationOwner'],
    displayName: 'Vendors',
    iconPath: './icons/Vendor.png',
  },
  {
    path: '../../board/mainBoard/organizationEmployee/myAssetRequests',
    acceptableRoles: ['OrganizationOwner', 'OrganizationAssetManager', 'OrganizationEmployee'],
    displayName: 'My Assets Requests',
    iconPath: './icons/AssetRequests.png',
  },
  {
    path: '/board/mainBoard/vendor/vendorDashboard',
    acceptableRoles: ['Vendor'],
    displayName: 'Dashboard',
    iconPath: './icons/Dashboard.png',
  },
  {
    path: '../../board/mainBoard/organizationAdmin/organizationSettings',
    acceptableRoles: ['OrganizationAssetManager'],
    displayName: 'Settings',
    iconPath: './icons/Settings.png',
  },
  {
    path: '../../board/mainBoard/organizationEmployee/settings',
    acceptableRoles: ['OrganizationEmployee'],
    displayName: 'Settings',
    iconPath: './icons/Settings.png',
  },
  {
    path: '/board/mainBoard/vendor/vendorSettings',
    acceptableRoles: ['Vendor'],
    displayName: 'Settings',
    iconPath: './icons/Settings.png',
  },
  // {
  //   path: 'organizationVendorChatBox',
  //   acceptableRoles: ['OrganizationOwner', 'Vendor'],
  //   displayName: 'OV-Chat',
  //   iconPath: './icons/OVChat.png',
  // },
];
