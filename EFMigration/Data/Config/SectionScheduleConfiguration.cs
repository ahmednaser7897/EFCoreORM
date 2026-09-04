using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class SectionScheduleConfiguration : IEntityTypeConfiguration<SectionSchedule>
{
    public void Configure(EntityTypeBuilder<SectionSchedule> builder)
    {
        //IN SQL : Id INT PRIMARY KEY
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        builder.Property(x => x.StartTime).HasColumnName("StartTime").HasColumnType("TIME");
        builder.Property(x => x.EndTime).HasColumnName("EndTime").HasColumnType("TIME");



        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        builder.HasData(LoadSectionSchedules());

        //GET THE TABLE NAME
        builder.ToTable("SectionSchedules");
    }

    private static List<SectionSchedule> LoadSectionSchedules()
    {
        // IN SQL :
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (1, 1, 1, '08:00:00', '10:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (2, 2, 3, '14:00:00', '18:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (3, 3, 4, '10:00:00', '15:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (4, 4, 1, '10:00:00', '12:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (5, 5, 1, '16:00:00', '18:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (6, 6, 2, '08:00:00', '10:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (7, 7, 3, '11:00:00', '14:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (8, 8, 4, '10:00:00', '14:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (9, 9, 4, '16:00:00', '18:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (10, 10, 3, '12:00:00', '15:00:00');
        // INSERT INTO SectionSchedules (Id, SectionId, ScheduleId, StartTime, EndTime) VALUES (11, 11, 5, '09:00:00', '11:00:00');
        return
                [
                    new SectionSchedule { Id = 1, SectionId= 1, ScheduleId= 1, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(10) },
                    new SectionSchedule { Id = 2, SectionId= 2, ScheduleId= 3, StartTime = TimeSpan.FromHours(14), EndTime = TimeSpan.FromHours(18) },
                    new SectionSchedule { Id = 3, SectionId= 3, ScheduleId= 4, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(15) },
                    new SectionSchedule { Id = 4, SectionId= 4, ScheduleId= 1, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(12) },
                    new SectionSchedule { Id = 5, SectionId= 5, ScheduleId= 1, StartTime = TimeSpan.FromHours(16), EndTime = TimeSpan.FromHours(18) },
                    new SectionSchedule { Id = 6, SectionId= 6, ScheduleId= 2, StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(10) },
                    new SectionSchedule { Id = 7, SectionId= 7, ScheduleId= 3, StartTime = TimeSpan.FromHours(11), EndTime = TimeSpan.FromHours(14) },
                    new SectionSchedule { Id = 8, SectionId= 8, ScheduleId= 4, StartTime = TimeSpan.FromHours(10), EndTime = TimeSpan.FromHours(14) },
                    new SectionSchedule { Id = 9, SectionId= 9, ScheduleId= 4, StartTime = TimeSpan.FromHours(16), EndTime = TimeSpan.FromHours(18) },
                    new SectionSchedule { Id = 10, SectionId= 10, ScheduleId= 3, StartTime = TimeSpan.FromHours(12), EndTime = TimeSpan.FromHours(15) },
                    new SectionSchedule { Id = 11, SectionId= 11, ScheduleId= 5, StartTime = TimeSpan.FromHours(9), EndTime = TimeSpan.FromHours(11) }
                ];
    }

}
