using System.Diagnostics.Contracts;

namespace BlagoDiy.DataAccessLayer.Entites;

public class Achievement
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public int Destination { get; set; }
    
    public List<AchievementToUser> Users { get; set; }
}