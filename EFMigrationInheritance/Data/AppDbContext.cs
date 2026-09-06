using EFMigrationInheritance.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFMigrationInheritance.Data;

public class AppDbContext : DbContext
{
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Individual> Individuals { get; set; }
    public DbSet<Coporate> Coporates { get; set; }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<MultipleChoiceQuiz> MultipleChoiceQuizzes { get; set; }
    public DbSet<TrueAndFalseQuiz> TrueAndFalseQuizzes { get; set; }

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
