using AssetIn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<OrganizationsAssetAssignReturn> OrganizationsAssetAssignReturns { get; set; }
    public DbSet<OrganizationsAssetCatagory> OrganizationsAssetCatagories { get; set; }
    public DbSet<OrganizationsAssetMaintanence> OrganizationsAssetMaintanences { get; set; }
    public DbSet<OrganizationsAssetRequest> OrganizationsAssetRequests { get; set; }
    public DbSet<OrganizationsAssetRetirement> OrganizationsAssetRetirements { get; set; }
    public DbSet<OrganizationsAssetStatus> OrganizationsAssetStatuses { get; set; }
    public DbSet<OrganizationsAssetType> OrganizationsAssetTypes { get; set; }
    public DbSet<OrganizationsDomain> OrganizationsDomains { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<VendorProcurementDetail> VendorProcurementDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Asset>().HasIndex(a=> a.AssetIdentificationNumber).IsUnique();
        modelBuilder.Entity<Asset>().HasIndex(a=> a.SerialNumber).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u=> u.Email).IsUnique();
        modelBuilder.Entity<Vendor>().HasIndex(u=> u.Email).IsUnique();
    }
}