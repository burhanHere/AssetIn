import { Injectable } from '@angular/core';
import * as XLSX from 'xlsx';

@Injectable({
  providedIn: 'root'
})
export class HelperFunctionService {

  public exportAssetList(assetArray: any[], filename: string = 'asset-list.csv'): void {
    if (!assetArray || assetArray.length === 0) return;

    // Step 1: Convert JSON to worksheet
    const worksheet = XLSX.utils.json_to_sheet(assetArray);

    // Step 2: Convert worksheet to CSV string
    const csv = XLSX.utils.sheet_to_csv(worksheet);

    // Step 3: Create a Blob and trigger download using anchor element
    const blob = new Blob([csv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);

    const link = document.createElement('a');
    link.href = url;
    link.setAttribute('download', filename);
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    // Optional: Revoke URL for memory cleanup
    URL.revokeObjectURL(url);
  }


}
