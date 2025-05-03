using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsAssetRequest
{
    [Key]
    public int OrganizationsAssetRequestID { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public DateTime RequestDate { get; set; }
    [Required]
    public int RequestStatus { get; set; }
    [Required]
    public DateTime? RequestProcessedDate { get; set; }
    [Required]
    public bool CompletionStatus { get; set; }
    [Required]
    public DateTime? RequestCompletedDate { get; set; }
    [Required]
    public string UserID { get; set; }
    [Required]
    public int OrganizationID { get; set; }
    [ForeignKey("OrganizationID")]
    public Organization Organization { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; }
}
