using System.ComponentModel.DataAnnotations;

namespace BlagoDiy.BusinessLogic.Models;

public class DonationPost
{
    
    public string? Name { get; set; }
    
    [EmailAddress]
    public string? Email { get; set; }
    
    [Range(0, int.MaxValue)]
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    
    
    public int CampaignId { get; set; }
    public int? UserId { get; set; }

}