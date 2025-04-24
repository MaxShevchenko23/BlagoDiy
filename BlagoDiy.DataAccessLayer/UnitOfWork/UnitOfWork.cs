using BlagoDiy.DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BlagoDiy.DataAccessLayer.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlagoContext context;

    public UnitOfWork(BlagoContext _dbContext)
    {
        context = _dbContext;
    }

    public CampaignRepository CampaignRepository => new(context);
    
    public DonationRepository DonationRepository => new(context);
    public UserRepository UserRepository => new(context);
    public AchievementRepository AchievementRepository => new(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
    
}