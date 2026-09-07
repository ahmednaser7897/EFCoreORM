using EFQueryData.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFQueryData.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Office> Offices { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Participant> Particpants { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder builder)
        {
            base.ConfigureConventions(builder);
            builder.Properties<DateOnly>()
                .HaveConversion<Config.DateOnlyConverter>()
                .HaveColumnType("date");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    ConnectionString.LoadConnectionString(),
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                    )
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                ;
            // Enable Lazy Loading
            //.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // modelBuilder.ApplyConfiguration(new CourseConfiguration()); // not best practice

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
