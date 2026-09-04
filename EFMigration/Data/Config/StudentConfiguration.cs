using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : FName VARCHAR(50) NOT NULL,
        // IN SQL : LName VARCHAR(50) NOT NULL,
        builder.Property(x => x.FName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 50
        builder.Property(x => x.LName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 50

        // LOAD THE INIT VALUES FOR THE STUDENT TABLE
        builder.HasData(LoadStudents());

        //GET THE TABLE NAME
        builder.ToTable("Students");
    }
    private static List<Student> LoadStudents()
    {
        // IN SQL :
        //         INSERT INTO Students (Id, Name) VALUES (1, 'Fatima', 'Ali');
        //         INSERT INTO Students (Id, Name) VALUES (2, 'Noor', 'Saleh');
        //         INSERT INTO Students (Id, Name) VALUES (3, 'Omar', 'Youssef');
        //         INSERT INTO Students (Id, Name) VALUES (4, 'Huda', 'Ahmed');
        //         INSERT INTO Students (Id, Name) VALUES (5, 'Amira', 'Tariq');
        //         INSERT INTO Students (Id, Name) VALUES (6, 'Zainab', 'Ismail');
        //         INSERT INTO Students (Id, Name) VALUES (7, 'Yousef', 'Farid');
        //         INSERT INTO Students (Id, Name) VALUES (8, 'Layla', 'Mustafa');
        //         INSERT INTO Students (Id, Name) VALUES (9, 'Mohammed', 'Adel');
        //         INSERT INTO Students (Id, Name) VALUES (10, 'Samira', 'Nabil');
        return
                [
                    new Student { Id = 1, FName = "Fatima", LName = "Ali"},
                    new Student { Id = 2, FName = "Noor", LName = "Saleh"},
                    new Student { Id = 3, FName = "Omar", LName = "Youssef"},
                    new Student { Id = 4, FName = "Huda", LName = "Ahmed"},
                    new Student { Id = 5, FName = "Amira", LName = "Tariq"},
                    new Student { Id = 6, FName = "Zainab", LName = "Ismail"},
                    new Student { Id = 7, FName = "Yousef", LName = "Farid"},
                    new Student { Id = 8, FName = "Layla", LName = "Mustafa"},
                    new Student { Id = 9, FName = "Mohammed", LName = "Adel"},
                    new Student { Id = 10, FName = "Samira", LName = "Nabil"}
                ];
    }

}
