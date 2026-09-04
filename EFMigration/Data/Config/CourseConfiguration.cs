using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        //IN SQL : Id INT PRIMARY KEY
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : CourseName VARCHAR(255) NOT NULL,
        //builder.Property(x => x.CourseName).HasMaxLength(255);// nvarchar(255)
        builder.Property(x => x.CourseName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 

        // IN SQL : Price DECIMAL(15, 2) NOT NULL
        builder.Property(x => x.Price).IsRequired().HasPrecision(15, 2);// set the price to required and max length of 18 and 2 decimal places

        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        // IN SQL :
        // INSERT INTO Courses (Id, CourseName, Price) VALUES (1, 'Mathematics', 1000.00);
        // INSERT INTO Courses (Id, CourseName, Price) VALUES (2, 'Physics', 2000.00);
        // INSERT INTO Courses (Id, CourseName, Price) VALUES (3, 'Chemistry', 1500.00);
        // INSERT INTO Courses (Id, CourseName, Price) VALUES (4, 'Biology', 1200.00);
        // INSERT INTO Courses (Id, CourseName, Price) VALUES (5, 'Computer Science', 3000.00);
        builder.HasData(LoadCourses());

        //GET THE TABLE NAME
        builder.ToTable("Courses");
    }

    private static List<Course> LoadCourses()
    {
        return
                [
                    new Course { Id = 1, CourseName = "Mathematics", Price = 1000.00m },
                    new Course { Id = 2, CourseName = "Physics", Price = 2000.00m },
                    new Course { Id = 3, CourseName = "Chemistry", Price = 1500.00m },
                    new Course { Id = 4, CourseName = "Biology", Price = 1200.00m },
                    new Course { Id = 5, CourseName = "Computer Science", Price = 3000.00m }
                ];
    }

}
