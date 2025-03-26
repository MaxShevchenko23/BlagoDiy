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

    public override async Task<Donation> GetByIdAsync(int id)
    {
        return await context.Donations.FindAsync(id);
    }

    public override async Task<IEnumerable<Donation>> GetAllAsync()
    {
        return await context.Donations.ToListAsync();
    }

    public override async Task AddAsync(Donation entity)
    {
        await context.Donations.AddAsync(entity);
        await context.SaveChangesAsync();
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
}