using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssetIn.Server.Controllers;

[ApiController]
[Route("AssetIn.Server/[controller]")]
[Authorize(Policy = "OrganizationOwnerOrganizationAssetManagerPolicy")]
public class EmployeeManagementController : ControllerBase
{
}