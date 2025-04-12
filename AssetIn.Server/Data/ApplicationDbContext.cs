using AssetIn.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
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
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<VendorProcurementDetail> VendorProcurementDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Asset>().HasIndex(a => a.AssetIdentificationNumber).IsUnique();
        modelBuilder.Entity<Asset>().HasIndex(a => a.SerialNumber).IsUnique();
        modelBuilder.Entity<Vendor>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "OrganizationOwner", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "ORGANIZATIONOWNER" },
            new IdentityRole { Id = "2", Name = "OrganizationEmployee", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "ORGANIZATIONEMPLOYEE" },
            new IdentityRole { Id = "3", Name = "OrganizationAssetManager", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "ORGANIZATIONASSETMANAGER" },
            new IdentityRole { Id = "4", Name = "Vendor", ConcurrencyStamp = Guid.NewGuid().ToString(), NormalizedName = "VENDOR" }
        );

        modelBuilder.Entity<OrganizationsAssetStatus>().HasData(
            new OrganizationsAssetStatus { OrganizationsAssetStatusID = 1, OrganizationsAssetStatusName = "Assigned" },
            new OrganizationsAssetStatus { OrganizationsAssetStatusID = 2, OrganizationsAssetStatusName = "Retired" },
            new OrganizationsAssetStatus { OrganizationsAssetStatusID = 4, OrganizationsAssetStatusName = "Available" },
            new OrganizationsAssetStatus { OrganizationsAssetStatusID = 3, OrganizationsAssetStatusName = "UnderMaintenance" }
        );
    }
}