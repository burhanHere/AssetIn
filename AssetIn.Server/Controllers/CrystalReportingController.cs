using System.Drawing;
using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Repositories;
using AssetIn.Server.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
public class CrystalReportingController(ApplicationDbContext applicationDbContext, CrystalReportingService crystalReportingService, IConverter converter) : ControllerBase
{
    private readonly CrystalReportingRepository crystalReportingRepository = new(applicationDbContext);
    private readonly CrystalReportingService crystalReportingService = crystalReportingService;
    private readonly IConverter _converter = converter;

    [HttpGet("GenerateHtmlReportForAssets")]
    public async Task<IActionResult> GenerateHtmlReportForAssets()
    {
        var assets = await crystalReportingRepository.GetAssetsAsync();
        if (assets == null)
        {
            return NotFound(new ApiResponse()
            {
                Status = StatusCodes.Status204NoContent,
                ResponseData = new List<string> { "Error", "No assets found to make report." }
            });
        }

        var htmlReport = crystalReportingService.GenerateHtmlForAsset(assets);

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = new GlobalSettings
            {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Portrait,
                Margins = new MarginSettings { Top = 10, Bottom = 10 },
            },
            Objects = { new ObjectSettings { HtmlContent = htmlReport } }
        };

        var pdf = _converter.Convert(doc);

        return Ok(new ApiResponse()
        {
            Status = StatusCodes.Status200OK,
            ResponseData = File(pdf, "application/pdf", "BooksReport.pdf")// file is Base64 encoded string, so have to convert manuyally to a pdf file on the client side
        });
    }

}