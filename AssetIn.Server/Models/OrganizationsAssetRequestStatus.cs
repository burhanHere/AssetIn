using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetRequestStatus
{
    [Key]
    public int OrganizationsAssetRequestStatusID { get; set; }
    [Required]
    public string OrganizationsAssetRequestStatusName { get; set; }
}
