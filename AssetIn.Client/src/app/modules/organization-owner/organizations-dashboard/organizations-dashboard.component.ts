import { Component } from '@angular/core';

@Component({
  selector: 'app-organizations-dashboard',
  templateUrl: './organizations-dashboard.component.html',
  styleUrl: './organizations-dashboard.component.css',
})
export class OrganizationsDashboardComponent {
  public organizations: Array<any> = [
    {
      name: 'Systems Limited',
      employees: 5320,
      assets: 1500,
      assetWorth: '$10M',
    },
    { name: 'Tech Corp', employees: 3210, assets: 800, assetWorth: '$7M' },
    {
      name: 'Global Solutions',
      employees: 2750,
      assets: 950,
      assetWorth: '$5M',
    },
    {
      name: 'Prime Innovations',
      employees: 4000,
      assets: 1200,
      assetWorth: '$9M',
    },
  ];

  public addNewOrganization(): void {
    this.organizations.push({
      name: 'New Organization',
      employees: 0,
      assets: 0,
      assetWorth: '$0',
    });
  }
}
