using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlagoDiy.DataAccessLayer.Entites;

public class Donation : IEntity
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int CampaignId { get; set; }
    
    [ForeignKey("CampaignId")]
    public Campaign Campaign { get; set; }
    
    public int? UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }
}