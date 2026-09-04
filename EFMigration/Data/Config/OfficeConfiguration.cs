using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class OfficeConfiguration : IEntityTypeConfiguration<Office>
{
    public void Configure(EntityTypeBuilder<Office> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : OfficeName VARCHAR(50) NOT NULL,
        builder.Property(x => x.OfficeName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 
        // IN SQL : OfficeLocation VARCHAR(50) NOT NULL,
        builder.Property(x => x.OfficeLocation).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 


        // LOAD THE INIT VALUES FOR THE OFFICE TABLE
        builder.HasData(LoadOffices());

        //GET THE TABLE NAME
        builder.ToTable("Offices");
    }
    private static List<Office> LoadOffices()
    {
        // IN SQL :
        // INSERT INTO Offices (Id, OfficeName, OfficeLocation) VALUES (1, 'Off_05', 'building A');
        // INSERT INTO Offices (Id, OfficeName, OfficeLocation) VALUES (2, 'Off_12', 'building B');
        // INSERT INTO Offices (Id, OfficeName, OfficeLocation) VALUES (3, 'Off_32', 'Adminstration');
        // INSERT INTO Offices (Id, OfficeName, OfficeLocation) VALUES (4, 'Off_44', 'IT Department');
        // INSERT INTO Offices (Id, OfficeName, OfficeLocation) VALUES (5, 'Off_43', 'IT Department');
        return
                [
                    new Office { Id = 1, OfficeName = "Off_05", OfficeLocation = "building A" },
                    new Office { Id = 2, OfficeName = "Off_12", OfficeLocation = "building B" },
                    new Office { Id = 3, OfficeName = "Off_32", OfficeLocation = "Adminstration" },
                    new Office { Id = 4, OfficeName = "Off_44", OfficeLocation = "IT Department" },
                    new Office { Id = 5, OfficeName = "Off_43", OfficeLocation = "IT Department" }
                ];
    }

}
