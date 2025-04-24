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
    
    public DbSet<Achievement> Achievements { get; set; }
    
    public DbSet<AchievementToUser> AchievementToUsers { get; set; }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Campaign>()
            .Property(c => c.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Campaign>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.CreatorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Donation>()
            .Property(d => d.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        modelBuilder.Entity<Donation>()
            .HasOne(d => d.Campaign)
            .WithMany(c => c.Donations)
            .HasForeignKey(d => d.CampaignId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");


        modelBuilder.Entity<AchievementToUser>()
            .HasKey(e => e.Id);

        modelBuilder.Entity<AchievementToUser>()
            .HasOne(e => e.User)
            .WithMany(e => e.Achievements)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AchievementToUser>()
            .HasOne(e => e.Achievement)
            .WithMany(e => e.Users)
            .HasForeignKey(e => e.AchievementId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
}