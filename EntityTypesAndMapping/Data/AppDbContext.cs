using EntityTypesAndMapping.Data;
using EntityTypesAndMapping.Entities;
using Microsoft.EntityFrameworkCore;


namespace EntityTypesAndMapping.Data;

internal class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    // i can ignore OrderDetail entity from here also
    // but it will still be created as a table
    // becuase Order class has a collection of OrderDetail 
    // and EF Core will automatically create the table for the collection
    // but it will not be seen to AppDbContext 
    // we must get one order to get order details
    // but we will get it 
    public DbSet<OrderDetail> OrderDetails { get; set; }
    // Map View instead of table
    public DbSet<OrderWithDetailsView> OrderWithDetailsViews { get; set; }
    // Map View with function
    public DbSet<OrderBill> OrderBills { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionString());
    }
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Set Default schema for all entities
        // so that it will create all tables in the default schema
        modelBuilder.HasDefaultSchema("Sales");
        modelBuilder.Entity<Product>().ToTable("Products", "Inventory").HasKey(p => p.Id);
        // modelBuilder.Entity<Order>().ToTable("Orders", "Sales").HasKey(o => o.Id);
        // modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails", "Sales").HasKey(od => od.Id);
        // this is also work  like [NotMapped] but it is more readable and maintainable
        // modelBuilder.Ignore<Snapshot>();
        // now its exist on the ef core 
        // but not as a dataset
        modelBuilder.Entity<AuditEntry>().HasKey(ae => ae.Id);

        // map OrderWithDetailsView to a view in the database
        modelBuilder.Entity<OrderWithDetailsView>()
        .ToView("OrderWithDetailsView", schema: "dbo")
        .HasNoKey();

        // map OrderBill to a view in the database
        modelBuilder.Entity<OrderBill>()
        .ToFunction("OrderBill")
        .HasNoKey();


    }
}
