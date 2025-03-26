namespace BlagoDiy.BusinessLogic.Models;

public class DonationGet
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public decimal Amount { get; set; }
    public string? Message { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public int CampaignId { get; set; }
    
}