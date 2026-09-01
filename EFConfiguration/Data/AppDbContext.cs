using EFConfiguration.Data.Config;
using EFConfiguration.Entities;
using Microsoft.EntityFrameworkCore;
namespace EFConfiguration.Data
{

    //Configuration By Convention
    // it works by making a rules
    // 1- THE Table name 
    //    - by default it will be the same as the DbSet property name plural
    //    - we can change it by using OnModelCreating 

    // 2- Each property must be with the same name as table column
    //    - if it not the same name EF will case an error in runtime
    //    - it dose not have to case sensitive  

    // 3- primary key
    // - EF will find primary key if property name is Id, id , ID or [{ClassName}Id] like UserId

    // 4- data type
    // - EF will map the data type to the database by default
    // - DateTime -> DateTime2
    // - decimal -> decimal(18,2)
    // - string -> nvarchar(max)

    internal class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tweet> Tweets { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionStringV2());
        }
    }

    //Configuration By Data Annotation
    // we use attributes to configure the model
    // it is more readable than convention but less flexible than fluent api
    // data annotation is a good option for simple configuration
    // but for complex configuration we should use fluent api

    internal class AppDbContextWithAnnotation : DbContext
    {
        public DbSet<UserWithAnnotation> Users { get; set; } = null!;
        public DbSet<TweetWithAnnotation> Tweets { get; set; } = null!;
        public DbSet<CommentWithAnnotation> Comments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionStringV1());
        }
    }
    internal class AppDbContextWithFluentApi : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Tweet> Tweets { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        // we can add in the same file all configuration for all entity types
        // OR we can create a separate file for each entity type configuration
        // and then add it to the OnModelCreating method

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Entity<User>().ToTable("tblUsers");
            // modelBuilder.Entity<User>().HasKey(x => x.UserId);
            // modelBuilder.Entity<User>().Property(x => x.Username).IsRequired().HasMaxLength(100);
            // modelBuilder.Entity<Tweet>().ToTable("tblTweets");
            // modelBuilder.Entity<Tweet>().HasKey(x => x.TweetId);
            // modelBuilder.Entity<Tweet>().Property(x => x.TweetText).IsRequired().HasMaxLength(100);
            // modelBuilder.Entity<Comment>().ToTable("tblComments");
            // modelBuilder.Entity<Comment>().HasKey(x => x.CommentId);
            // modelBuilder.Entity<Comment>().Property(x => x.CommentText).IsRequired().HasMaxLength(100);

            // if we did not used OnModelCreating to configure the model
            // we can use ApplyConfigurationsFromAssembly method
            new UserConfig().Configure(modelBuilder.Entity<User>());
            new TweetConfig().Configure(modelBuilder.Entity<Tweet>());
            new CommentConfig().Configure(modelBuilder.Entity<Comment>());
            // or instead of this 
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContextWithFluentApi).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionStringV1());
        }
    }

}
