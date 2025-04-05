using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;

namespace BlagoDiy.DataAccessLayer.Repositories;

public class UserRepository: Repository<User>
{
    private readonly BlagoContext context;
    
    public UserRepository(BlagoContext _context)
    {
        context = _context;
    }

    public override async Task<User> GetByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public override async Task AddAsync(User entity)
    {
        await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public override async Task UpdateAsync(User entity)
    {
        context.Users.Update(entity);
        await context.SaveChangesAsync();
    }

    public override async Task DeleteAsync(int id)
    {
        var entity = await context.Users.FindAsync(id);
        
        if (entity != null)
        {
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
    
    public async Task<User?> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        return await context.Users
            .FirstOrDefaultAsync(u => u.Email == email 
                                      && u.Password == password);
    }
}