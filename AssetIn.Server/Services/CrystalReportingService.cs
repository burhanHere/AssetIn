using AssetIn.Server.Models;

namespace AssetIn.Server.Services;

public class CrystalReportingService
{
    public string GenerateHtmlForAsset(List<Asset> assets)
    {
        var html = @"
        <style>
            body { font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; margin: 20px; background-color: #f5f5f5; }
            .report-container { background-color: white; padding: 30px; border-radius: 8px; box-shadow: 0 2px 10px rgba(0,0,0,0.1); }
            h2 { color: #2c3e50; text-align: center; margin-bottom: 30px; font-size: 28px; font-weight: 300; }
            table { width: 100%; border-collapse: collapse; margin-top: 20px; }
            th { background-color: #3498db; color: white; padding: 12px; text-align: left; font-weight: 500; border: none; }
            td { padding: 10px 12px; border-bottom: 1px solid #ecf0f1; }
            tr:nth-child(even) { background-color: #f8f9fa; }
            tr:hover { background-color: #e8f4fd; }
            .table-wrapper { overflow-x: auto; }
        </style>
        <div class='report-container'>
            <h2>Asset Report</h2>
            <div class='table-wrapper'>
            <table>
                <tr>
                <th>ID</th><th>Name</th><th>Serial</th><th>Barcode</th><th>Model</th><th>Manufacturer</th><th>Added to Records</th><th>Last Updated</th><th>Purchase Date</th><th>Price</th><th>Cost</th><th>Location</th>
                </tr>";

        foreach (var asset in assets)
        {
            html += $"<tr><td>{asset.AssetlD}</td><td>{asset.AssetName}</td><td>{asset.SerialNumber}</td><td>{asset.Barcode}</td><td>{asset.Model}</td><td>{asset.Manufacturer}</td><td>{asset.CreatedDate:yyyy-MM-dd}</td><td>{asset.UpdatedDate:yyyy-MM-dd}</td><td>{asset.PurchaseDate:yyyy-MM-dd}</td><td>{asset.PurchasePrice:C}</td><td>{asset.CostPrice:C}</td><td>{asset.Location}</td></tr>";
        }

        html += "</table></div></div>";
        return html;
    }
}