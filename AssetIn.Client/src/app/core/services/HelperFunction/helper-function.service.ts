import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HelperFunctionService {

  // Helper function
  public exportAssetList(assetArray: any[], filename: string = 'asset-list.csv'): void {
    const csvData = this.convertToCSV(assetArray);
    const blob = new Blob([csvData], { type: 'text/csv;charset=utf-8;' });

    const link = document.createElement('a');
    const url = URL.createObjectURL(blob);
    link.href = url;
    link.setAttribute('download', filename);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
  }

  private convertToCSV(objArray: any[]): string {
    if (!objArray.length) {
      return '';
    }

    const header = Object.keys(objArray[0]);
    const csvRows = [
      header.join(','), // Header row
      ...objArray.map(obj =>
        header.map(field => {
          let cell = obj[field] ?? '';

          if (typeof cell === 'string' && (field.toLowerCase().includes('date') || field.toLowerCase().includes('barcode') || field.toLowerCase().includes('lastupdated'))) {
            cell = `'${cell}'`;
          }

          return `"${cell.toString().replace(/"/g, '""')}"`;
        }).join(',')
      )
    ];

    return csvRows.join('\r\n');
  }
  
}
