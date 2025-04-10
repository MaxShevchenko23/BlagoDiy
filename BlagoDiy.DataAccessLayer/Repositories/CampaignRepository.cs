﻿using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlagoDiy.DataAccessLayer.Repositories;

public class CampaignRepository : Repository<Campaign>
{
    private readonly BlagoContext context;

    public CampaignRepository(BlagoContext _context)
    {
        context = _context;
    }

    public override async Task<Campaign> GetByIdAsync(int id)
    {
        return await context
            .Campaigns
            .Include(e=>e.User)
            .FirstAsync(e => e.Id == id);
    }
    
    public override async Task<IEnumerable<Campaign>> GetAllAsync()
    {
        return await context.Campaigns.ToListAsync();
    }
    
    public async Task<IEnumerable<Campaign>> GetAllPaginatedAsync(int page, int pageSize)
    {
        return await context
            .Campaigns
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
    
    public override async Task AddAsync(Campaign entity)
    {
        await context.Campaigns.AddAsync(entity);
        await context.SaveChangesAsync();
    }
    
    public override async Task UpdateAsync(Campaign entity)
    {
        context.Campaigns.Update(entity);
        await context.SaveChangesAsync();
    }
    
    public override async Task DeleteAsync(int id)
    {
        var entity = await context.Campaigns.FindAsync(id);
        if (entity != null)
        {
            context.Campaigns.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}