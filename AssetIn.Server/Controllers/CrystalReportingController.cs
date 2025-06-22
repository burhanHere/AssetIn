using System.Drawing;
using AssetIn.Server.Data;
using AssetIn.Server.DTOs;
using AssetIn.Server.Helpers;
using AssetIn.Server.Models;
using AssetIn.Server.Repositories;
using AssetIn.Server.Services;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
public class CrystalReportingController(ApplicationDbContext applicationDbContext, CrystalReportingService crystalReportingService, IConverter converter, UserManager<User> userManager) : ControllerBase
{
    private readonly CrystalReportingRepository _crystalReportingRepository = new(applicationDbContext, userManager);
    private readonly CrystalReportingService _crystalReportingService = crystalReportingService;
    private readonly IConverter _converter = converter;

    [HttpGet("GetFilterData")]
    public async Task<IActionResult> GetFilterData(int organizationId)
    {
        var result = await _crystalReportingRepository.GetFilterData(organizationId);
        return HelperFunctions.ResponseFormatter(this, result);
    }

    [HttpGet("GenerateHtmlReportByFilter")]
    public async Task<IActionResult> GenerateHtmlReportByFilter(ReportingFilterDto reportingFilterDto)
    {
        if (reportingFilterDto.reportType == "Assets")
        {
            var result = await _crystalReportingRepository.GetAssetsReportDataAsync(reportingFilterDto.assetType, reportingFilterDto.assetStatus, reportingFilterDto.assetCategory, reportingFilterDto.assignedTo, reportingFilterDto.toDate, reportingFilterDto.fromDate, reportingFilterDto.OrganizationId);
            return HelperFunctions.ResponseFormatter(this, result);
        }
        else if (reportingFilterDto.reportType == "Employee")
        {
            var result = await _crystalReportingRepository.GetEmployeeReportDataAsync(reportingFilterDto.employeeRole, reportingFilterDto.employeeStatus, reportingFilterDto.specificEmployee, reportingFilterDto.gender, reportingFilterDto.OrganizationId, reportingFilterDto.toDate, reportingFilterDto.fromDate);
            return HelperFunctions.ResponseFormatter(this, result);
        }
        else if (reportingFilterDto.reportType == "Assets Request")
        {
            var result = await _crystalReportingRepository.GetAssetsRequestsReportDataAsync(reportingFilterDto.requestStatus, reportingFilterDto.requestedBy, reportingFilterDto.toDate, reportingFilterDto.fromDate, reportingFilterDto.OrganizationId);

            return HelperFunctions.ResponseFormatter(this, result);
        }
        else
        {
            return BadRequest(new ApiResponse()
            {
                Status = StatusCodes.Status400BadRequest,
                ResponseData = new List<string> { "Error", "Invalid report type." }
            });
        }
    }

    [HttpGet("GenerateHtmlReportForAssets")]
    public async Task<IActionResult> GenerateHtmlReportForAssets()
    {
        var assets = await _crystalReportingRepository.GetAssetsAsync();
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