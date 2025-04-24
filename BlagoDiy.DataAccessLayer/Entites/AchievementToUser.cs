namespace BlagoDiy.DataAccessLayer.Entites;

public class AchievementToUser
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AchievementId { get; set; }

    public int CurrentValue { get; set; }
    
    public User User { get; set; }
    public Achievement Achievement { get; set; }
}