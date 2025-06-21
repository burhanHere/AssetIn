using AssetIn.Server.Data;
using AssetIn.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace AssetIn.Server.Repositories;

public class CrystalReportingRepository(ApplicationDbContext applicationDbContext)
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;


    public async Task<List<Asset>> GetAssetsAsync()
    {
        List<Asset> assets = await _applicationDbContext.Assets
            .Where(x => !x.DeletedByOrganization)
            .ToListAsync();
        return assets;
    }
}