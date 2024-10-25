using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetMaintanence
{
    [Key]
    public int OrganizationsAssetMaintanenceID { get; set; }
    [Required]
    public DateTime SentDate { get; set; }
    [Required]
    public DateTime RetunDate { get; set; }
    [Required]
    public string Problem { get; set; }
    [Required]
    public string MaintanenceResult { get; set; }
    [Required]
    public int AssetID { get; set; }
    [ForeignKey("AssetID")]
    public Asset Asset { get; set; }
}
