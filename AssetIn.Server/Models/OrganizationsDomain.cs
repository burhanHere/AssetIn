using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssetIn.Server.Models;

public class OrganizationsDomain
{
    [Key]
    public int OrganizationsDomainID { get; set; }
    [Required]
    public string Domain { get; set; }
    [Required]
    public int OrganizationsID { get; set; }
    [ForeignKey("OrganizationsID")]
    public Organization Organization { get; set; }
}
