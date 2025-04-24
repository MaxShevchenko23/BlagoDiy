using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlagoDiy.DataAccessLayer.Repositories;

public class DonationRepository : Repository<Donation>
{
    private readonly BlagoContext context;

    public DonationRepository(BlagoContext _context)
    {
       context = _context;
    }

    public override async Task<Donation?> GetByIdAsync(int id)
    {
        return await context.Donations.FindAsync(id);
    }

    public override async Task<IEnumerable<Donation>> GetAllAsync()
    {
        return await context.Donations.ToListAsync();
    }

    public async Task<IEnumerable<Donation>> GetAllAsync(int page, int pageSize)
    {
        return await context.Donations.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }
    
    public override async Task AddAsync(Donation entity)
    {
        
        var campaign = await context.Campaigns.FindAsync(entity.CampaignId);
        campaign.Raised+=entity.Amount;
        
        await context.Donations.AddAsync(entity);
        context.Campaigns.Update(campaign);
        
        await context.SaveChangesAsync();

        var achievementRepo = new AchievementRepository(context);
        
        await achievementRepo.IncrementAchievementProgress(3, entity.UserId.Value, int.Parse(entity.Amount.ToString()));
        await achievementRepo.IncrementAchievementProgress(1, entity.UserId.Value, 1);

        if (await CheckIfUserDonatedToCategory(entity.UserId.Value, campaign.Category))
        {
            await achievementRepo.IncrementAchievementProgress(2, entity.UserId.Value, 1);
        }

    }

    public override async Task UpdateAsync(Donation entity)
    {
        context.Donations.Update(entity);
        await context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(int id)
    {
        var entity = await context.Donations.FindAsync(id);
        if (entity != null)
        {
            context.Donations.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task<IEnumerable<Donation>> GetDonationsByCampaignIdAsync(int campaignId, int take = 10)
    {
        var donations = context
            .Donations
            .Where(d => d.CampaignId == campaignId)
            .Include(e=>e.User)
            .Take(take);
        
        return await donations.ToListAsync();
    }
    
    public async Task<bool> CheckIfUserDonatedToCategory(int userId, string categoryId)
    {
        var donations = await context.Donations
            .Include(e=>e.Campaign)
            .Where(d => d.UserId == userId && d.Campaign.Category == categoryId)
            .ToListAsync();
        
        return donations.Any();
    }
}