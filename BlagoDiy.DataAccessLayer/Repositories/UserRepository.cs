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
        return await context.Users.AsNoTracking().FirstAsync(e => e.Id == id);
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public override async Task AddAsync(User entity)
    {
        var created = await context.Users.AddAsync(entity);
        await context.SaveChangesAsync();

        var acheivementsRepo = new AchievementRepository(context);

        var user = await GetUserByEmailAndPasswordAsync(entity.Email, entity.Password);
        
        await acheivementsRepo.InitializeAchievementsForUser(user.Id);
        
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
      var user= context.Users
            .Where(u => u.Email == email 
                                      && u.Password == password);
        
      
      return await user.FirstOrDefaultAsync();
    }
}