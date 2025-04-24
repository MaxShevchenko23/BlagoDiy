using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlagoDiy.DataAccessLayer.Repositories;

public class AchievementRepository : Repository<Achievement>
{
    private readonly BlagoContext context;

    public AchievementRepository(BlagoContext _context)
    {
        context = _context;
    }
    
    public override Task<Achievement?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public override async Task<IEnumerable<Achievement>> GetAllAsync()
    {
        var content = await context
            .Achievements.ToListAsync();
        
        return content;
    }

    public override async Task<Achievement> AddAsync(Achievement entity)
    {
        var created = await context.Achievements.AddAsync(entity);
        
        await context.SaveChangesAsync();
        var userIds = await context.Users.Select(e => e.Id).ToListAsync();
        
        var achievementsToUser = userIds.Select(e => new AchievementToUser()
        {
            UserId = e,
            AchievementId = created.Entity.Id,
            CurrentValue = 0
        });
        
        await context.AchievementToUsers.AddRangeAsync(achievementsToUser);
        await context.SaveChangesAsync();

        return created.Entity;
    }

    public override Task UpdateAsync(Achievement entity)
    {
        throw new NotImplementedException();
    }

    public override Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<List<AchievementToUser>> GetByUserIdAsync(int userId)
    {
        var achievements = await context
            .AchievementToUsers
            .Include(e => e.Achievement)
            .Where(e => e.UserId == userId)
            .ToListAsync();
        
        return achievements;
    }
    
    public async Task<IEnumerable<Achievement>> InitializeAchievementsForUser(int userId)
    {
        var achievements = context.Achievements;

        var achievementsToUser = achievements.Select(e => new AchievementToUser()
        {
            UserId = userId,
            AchievementId = e.Id,
            CurrentValue = 0
        });

        await context.AchievementToUsers.AddRangeAsync(achievementsToUser);
        
        return achievements;
    }
    
    public async Task IncrementAchievementProgress(int achievementId, int userId, int value)
    {
        var achievementToUser = 
            await context
            .AchievementToUsers
            .Include(e=>e.Achievement)
            .Where(e => e.UserId == userId && e.AchievementId == achievementId)
            .FirstAsync();

        if (achievementToUser.CurrentValue == achievementToUser.Achievement.Destination)
        {
            return;
        }
        
        achievementToUser.CurrentValue += value;
        
        if (achievementToUser.CurrentValue >= achievementToUser.Achievement.Destination)
        {
            achievementToUser.CurrentValue = achievementToUser.Achievement.Destination;
        }
        
        context.AchievementToUsers.Update(achievementToUser);
        await context.SaveChangesAsync();
    }
}