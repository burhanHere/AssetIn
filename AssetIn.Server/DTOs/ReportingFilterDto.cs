namespace AssetIn.Server.DTOs;

public class ReportingFilterDto
{
    public string? reportType { get; set; }
    // Asset Report
    public int assetType { get; set; }
    public int assetStatus { get; set; }
    public int assetCategory { get; set; }
    public string? assignedTo { get; set; }
    // Employee Report
    public string? employeeRole { get; set; }
    public bool employeeStatus { get; set; }
    public string? specificEmployee { get; set; }
    public string? gender { get; set; }
    // Asset Request Report
    public int requestStatus { get; set; }
    public string? requestedBy { get; set; }
    // date filters
    public DateTime? fromDate { get; set; }
    public DateTime? toDate { get; set; }
    public int OrganizationId { get; set; }

}