using BlagoDiy.DataAccessLayer.Entites;

namespace BlagoDiy.BusinessLogic.Models;

public class CampaignGetFull
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    
    
    public string Category { get; set; }
    
    public decimal Destination { get; set; }
    public decimal Raised { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? EndAt { get; set; }

    public string ImageUrl { get; set; }
    public string SocialUrls { get; set; }
    
    public IList<Donation> Donations { get; set; }
}