using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFMigration.Data;

public class AppDbContext : DbContext
{
    public DbSet<Course> Courses { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Student> Students { get; set; }
    // we disided not to add SectionSchedule table 
    // the relation between section and schedule is one to many
    // it is not many to many because one section can have only one schedule 
    // and one schedule can have many sections
    // so we can add ScheduleId to Section table
    //public DbSet<SectionSchedule> SectionSchedules { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString.LoadConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.ApplyConfiguration(new CourseConfiguration()); // not best practice
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
