using BlagoDiy.DataAccessLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlagoDiy.DataAccessLayer;

public class BlagoContext : DbContext
{
    public BlagoContext() : base()
    {
        
    }

    public BlagoContext(DbContextOptions<BlagoContext> options) : base(options)
    {
        var optionsBuilder = new DbContextOptionsBuilder<BlagoContext>(options);
           
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        
        optionsBuilder.UseSqlite(
            "Data Source=blago.db;Cache=Shared",
            b=>b.MigrationsAssembly("BlagoDiy.DataAccessLayer"));
        
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));

    }
    
    public DbSet<Campaign> Campaigns { get; set; }
    public DbSet<Donation> Donations { get; set; }
    public DbSet<User> Users { get; set; }
    
    
}