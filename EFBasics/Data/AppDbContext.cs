using EFBasics.Data;
using Microsoft.EntityFrameworkCore;
namespace EFCoreORM
{
    // DbContext is the bridge between database and code
    // DbContext is the main class of Entity Framework Core
    // it represents a session with the database
    // and can be use to query and save instances of our models
    // DbContext is a compination of the units of work and repository pattern
    //---------------------------------------------------------------------------------
    // to use DbContext we must set some configrations like:
    // 1. connection string
    // 2. tables
    // 3. entities
    // we can add it in the AppDbContext class by override OnConfiguring method
    // or we can adding it externall 
    // or using dependecy injection ServiceCollection
    //---------------------------------------------------------------------------------
    // 

    internal class AppDbContext : DbContext
    {
        // Represents a table of database
        public DbSet<Wallet> Wallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionString());
        }
    }

    internal class AppDbContextExternall : DbContext
    {
        public AppDbContextExternall(DbContextOptions options)
        : base(options) { }

        // Represents a table of database
        public DbSet<Wallet> Wallets { get; set; } = null!;
    }

}
