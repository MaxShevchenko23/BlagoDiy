using System.ComponentModel.DataAnnotations;

namespace BlagoDiy.DataAccessLayer.Entites;

public class User : IEntity
{
    [Key] 
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public List<Campaign> Campaigns { get; set; }
    public List<Donation> Donations { get; set; }
    public List<AchievementToUser> Achievements { get; set; }
    
    public DateTime CreatedAt { get; set; }
}