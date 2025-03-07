using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class Organization
{
    public int OrganizationID { get; set; }
    public string OrganizationName { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool ActiveOrganization { get; set; }
    [Required]
    public string UserID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; }
}
