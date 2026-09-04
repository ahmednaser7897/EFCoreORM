using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
{
    public void Configure(EntityTypeBuilder<Schedule> builder)
    {
        //IN SQL : Id INT PRIMARY KEY
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : Title VARCHAR(100) NOT NULL,
        builder.Property(x => x.Title).IsRequired().HasColumnType("VARCHAR").HasMaxLength(100);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 100 

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
                    new Schedule { Id = 1, Title = "Daily", SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 2, Title = "DayAfterDay", SUN = true, MON = false, TUE = true, WED = false, THU = true, FRI = false, SAT = false },
                    new Schedule { Id = 3, Title = "Twice-a-Week", SUN = false, MON = true, TUE = false, WED = true, THU = false, FRI = false, SAT = false },
                    new Schedule { Id = 4, Title = "Weekend", SUN = false, MON = false, TUE = false, WED = false, THU = false, FRI = true, SAT = true },
                    new Schedule { Id = 5, Title = "Compact", SUN = true, MON = true, TUE = true, WED = true, THU = true, FRI = true, SAT = true }
                ];
    }

}
