using System.ComponentModel.DataAnnotations;

namespace BlagoDiy.BusinessLogic.Models;

public class CampaignPost
{
    
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    
    public string Category { get; set; }
    
    
    [Range(1, int.MaxValue)]
    public decimal Destination { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public string ImageUrl { get; set; }
    public string SocialUrls { get; set; }
    
    public int CreatorId { get; set; }
    
}