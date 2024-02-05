using DomainLayer.Models.Complaints;
using DomainLayer.Models.Demands;
using DomainLayer.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RepositoryLayer.AppDbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(e => e.UserDetails)
                .WithOne()
                .HasForeignKey<UserExtendedDetails>(u => u.UserId)
                .IsRequired();

            modelBuilder.Entity<Complaint>()
                .HasMany(e => e.Demands)
                .WithOne()
                .HasForeignKey(e => e.ComplaintId)
                .IsRequired();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserExtendedDetails> UserExtendedDetails { get; set; }
        public DbSet<Complaint> Complaints { get; set; }
        public DbSet<Demand> Demands { get; set; }
    }

    public class AppDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(nameof(AppDbContext))
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
