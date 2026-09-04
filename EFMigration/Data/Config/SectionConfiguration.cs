using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class SectionConfiguration : IEntityTypeConfiguration<Section>
{
    public void Configure(EntityTypeBuilder<Section> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : Name SectionName(50) NOT NULL,
        builder.Property(x => x.SectionName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 

        // add new relationship with Course
        // courseId is FK ,so it is required
        builder.HasOne(x => x.Course)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.CourseId)
        //section must has a course (Required)
        .IsRequired();

        // add new relationship with Instructor
        // instructorId is FK ,so it is optional
        builder.HasOne(x => x.Instructor)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.InstructorId)
        //section may has an instructor (Optional)
        .IsRequired(false);

        // many to many relationship with Schedule via SectionSchedule table
        builder.HasMany(x => x.Schedules)
        .WithMany(x => x.Sections)
        .UsingEntity<SectionSchedule>();

        // many to many relationship with Students via Enrollment table
        builder.HasMany(x => x.Students)
       .WithMany(x => x.Sections)
       .UsingEntity<Enrollment>();



        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        builder.HasData(LoadSections());

        //GET THE TABLE NAME
        builder.ToTable("Sections");
    }
    private static List<Section> LoadSections()
    {
        // IN SQL :
        //  INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (1, 'S_MA1', 1, 1);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (2, 'S_MA2', 1, 2);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (3, 'S_PH1', 2, 1);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (4, 'S_PH2', 2, 3);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (5, 'S_CH1', 3, 2);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (6, 'S_CH2', 3, 3);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (7, 'S_BI1', 4, 4);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (8, 'S_BI2', 4, 5);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (9, 'S_CS1', 5, 4);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (10,'S_CS2', 5, 5);
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId) VALUES (11,'S_CS3', 5, 4);
        return
                [
                    new Section { Id = 1, SectionName = "S_MA1", CourseId = 1, InstructorId = 1 },
                    new Section { Id = 2, SectionName = "S_MA2", CourseId = 1, InstructorId = 2 },
                    new Section { Id = 3, SectionName = "S_PH1", CourseId = 2, InstructorId = 1 },
                    new Section { Id = 4, SectionName = "S_PH2", CourseId = 2, InstructorId = 3 },
                    new Section { Id = 5, SectionName = "S_CH1", CourseId = 3, InstructorId = 2 },
                    new Section { Id = 6, SectionName = "S_CH2", CourseId = 3, InstructorId = 3 },
                    new Section { Id = 7, SectionName = "S_BI1", CourseId = 4, InstructorId = 4 },
                    new Section { Id = 8, SectionName = "S_BI2", CourseId = 4, InstructorId = 5 },
                    new Section { Id = 9, SectionName = "S_CS1", CourseId = 5, InstructorId = 4 },
                    new Section { Id = 10, SectionName = "S_CS2", CourseId = 5, InstructorId = 5 },
                    new Section { Id = 11, SectionName = "S_CS3", CourseId = 5, InstructorId = 4 }
                ];
    }


}
