using EFMigration.Entities;
using EFMigration.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;
// we disided not to add SectionSchedule table 
// the relation between section and schedule is one to many
// it is not many to many because one section can have only one schedule 
// and one schedule can have many sections
// so we can add ScheduleId to Section table
public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        //IN SQL : Id INT PRIMARY KEY
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : Title VARCHAR(100) NOT NULL,
        //builder.Property(x => x.Title).IsRequired().HasColumnType("VARCHAR").HasMaxLength(100);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 100 
        // NOW its an ENUM SO NO NEED to set its type
        builder.Property(x => x.Title)
        .HasConversion(
            x => x.ToString(),
            x => (ScheduleEnum)Enum.Parse(typeof(ScheduleEnum), x)
        )
        .IsRequired();

        // IN SQL : BIT NOT NULL
        builder.Property(x => x.MON).IsRequired();
        builder.Property(x => x.TUE).IsRequired();
        builder.Property(x => x.WED).IsRequired();
        builder.Property(x => x.THU).IsRequired();
        builder.Property(x => x.FRI).IsRequired();
        builder.Property(x => x.SAT).IsRequired();
        builder.Property(x => x.SUN).IsRequired();


        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        builder.HasData(LoadSchedules());

        //GET THE TABLE NAME
        builder.ToTable("Schedules");
    }

    private static List<Schedule> LoadSchedules()
    {
        // IN SQL :
        // INSERT INTO Schedules (Id, Title, SUN, MON, TUE, WED, THU, FRI, SAT) VALUES (1, 'Daily', 1, 1, 1, 1, 1, 0, 0);
        // INSERT INTO Schedules (Id, Title, SUN, MON, TUE, WED, THU, FRI, SAT) VALUES (2, 'DayAfterDay', 1, 0, 1, 0, 1, 0, 0);
        // INSERT INTO Schedules (Id, Title, SUN, MON, TUE, WED, THU, FRI, SAT) VALUES (3, 'Twice-a-Week', 0, 1, 0, 1, 0, 0, 0);
        // INSERT INTO Schedules (Id, Title, SUN, MON, TUE, WED, THU, FRI, SAT) VALUES (4, 'Weekend', 0, 0, 0, 0, 0, 1, 1);
        // INSERT INTO Schedules (Id, Title, SUN, MON, TUE, WED, THU, FRI, SAT) VALUES (5, 'Compact', 1, 1, 1, 1, 1, 1, 1);
        return
                [
                    new Schedule { Id = 1, Title = ScheduleEnum.Daily, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 2, Title = ScheduleEnum.DayAfterDay, SUN = true, MON = false, TUE = true, WED = false, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 3, Title = ScheduleEnum.TwiceAWeek, SUN = false, MON = true, TUE = false, WED = true, THU = false, FRI = false, SAT = false },
                    new Schedule { Id = 4, Title = ScheduleEnum.Weekend, SUN = false, MON = false, TUE = false, WED = false, THU = false, FRI = true, SAT = true },
                    new Schedule { Id = 5, Title = ScheduleEnum.Compact, SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = true, SAT = true }
                ];
    }

}
