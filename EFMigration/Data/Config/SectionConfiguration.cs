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
        // we disided not to add SectionSchedule table 
        // the relation between section and schedule is one to many
        // it is not many to many because one section can have only one schedule 
        // and one schedule can have many sections
        // so we can add ScheduleId to Section table
        // builder.HasMany(x => x.Schedules)
        // .WithMany(x => x.Sections)
        // .UsingEntity<SectionSchedule>();
        builder.HasOne(x => x.Schedule)
        .WithMany(x => x.Sections)
        .HasForeignKey(x => x.ScheduleId)
        //section must has a schedule (Required)
        .IsRequired();


        // many to many relationship with Students via Enrollment table
        builder.HasMany(x => x.Students)
       .WithMany(x => x.Sections)
       .UsingEntity<Enrollment>();

        // we will not create a table for TimeSlot 
        // it is owned entity
        builder.OwnsOne(x => x.TimeSlot, y =>
        {
            y.Property(x => x.StartTime).HasColumnType("TIME").HasColumnName("StartTime");
            y.Property(x => x.EndTime).HasColumnType("TIME").HasColumnName("EndTime");
        });


        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        builder.HasData(LoadSections());

        //GET THE TABLE NAME
        builder.ToTable("Sections");
    }
    private static List<Section> LoadSections()
    {
        // IN SQL :
        //  INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (1, 'S_MA1', 1, 1,1, '08:00','10:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (2, 'S_MA2', 1, 2,3, '14:00','18:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (3, 'S_PH1', 2, 1,4, '10:00','15:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (4, 'S_PH2', 2, 3,1, '10:00','12:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (5, 'S_CH1', 3, 2,1, '16:00','18:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (6, 'S_CH2', 3, 3,2, '08:00','10:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (7, 'S_BI1', 4, 4,3, '11:00','14:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (8, 'S_BI2', 4, 5,4, '10:00','14:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (9, 'S_CS1', 5, 4,4, '16:00','18:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (10,'S_CS2', 5, 5,3, '12:00','15:00');
        // INSERT INTO Sections (Id, SectionName, CourseId, InstructorId,ScheduleId,StartTime,EndTime) VALUES (11,'S_CS3', 5, 4,5, '09:00','11:00');

        return
                [
                    new Section { Id = 1, SectionName = "S_MA1", CourseId = 1, InstructorId = 1,ScheduleId = 1,  StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(10)  }  ,
                    new Section { Id = 2, SectionName = "S_MA2", CourseId = 1, InstructorId = 2, ScheduleId = 3,  StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(18)},
                    new Section { Id = 3, SectionName = "S_PH1", CourseId = 2, InstructorId = 1,ScheduleId = 4,  StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(15) },
                    new Section { Id = 4, SectionName = "S_PH2", CourseId = 2, InstructorId = 3,ScheduleId = 1,  StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12) },
                    new Section { Id = 5, SectionName = "S_CH1", CourseId = 3, InstructorId = 2 ,ScheduleId = 1,  StartTime = TimeSpan.FromHours(16), EndTime = TimeSpan.FromHours(18)},
                    new Section { Id = 6, SectionName = "S_CH2", CourseId = 3, InstructorId = 3,ScheduleId = 2,  StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(10) },
                    new Section { Id = 7, SectionName = "S_BI1", CourseId = 4, InstructorId = 4 ,ScheduleId = 3,  StartTime = TimeSpan.FromHours(11), EndTime = TimeSpan.FromHours(14)},
                    new Section { Id = 8, SectionName = "S_BI2", CourseId = 4, InstructorId = 5,ScheduleId = 4, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(14) },
                    new Section { Id = 9, SectionName = "S_CS1", CourseId = 5, InstructorId = 4 ,ScheduleId = 4,  StartTime = TimeSpan.FromHours(16), EndTime = TimeSpan.FromHours(18)},
                    new Section { Id = 10, SectionName = "S_CS2", CourseId = 5, InstructorId = 5,ScheduleId = 3,  StartTime = TimeSpan.FromHours(12), EndTime = TimeSpan.FromHours(15)},
                    new Section { Id = 11, SectionName = "S_CS3", CourseId = 5, InstructorId = 4,ScheduleId = 5,  StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(11) }
                ];
    }


}
