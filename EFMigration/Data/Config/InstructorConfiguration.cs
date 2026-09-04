using EFMigration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFMigration.Data.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : Name VARCHAR(50) NOT NULL,
        //builder.Property(x => x.CourseName).HasMaxLength(255);// nvarchar(255)
        //builder.Property(x => x.Name).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 
        // no this is the fisrt change we will splite name to two columnsFName LName
        builder.Property(x => x.FName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255 
        builder.Property(x => x.LName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 255

        // add new relationship with offer
        // officeId is FK ,so it is optional
        builder.HasOne(x => x.Office)
        .WithOne(x => x.Instructor)
        .HasForeignKey<Instructor>(x => x.OfficeId)
        .IsRequired(false);


        // LOAD THE INIT VALUES FOR THE COURSE TABLE
        builder.HasData(LoadInstructorsv3());

        //GET THE TABLE NAME
        builder.ToTable("Instructors");
    }
    private static List<Instructor> LoadInstructorsv3()
    {
        // IN SQL :
        // INSERT INTO Instructors (Id, FName, LName,OfficeId ) VALUES (1, 'Ahmed', 'Abdullah',1);
        // INSERT INTO Instructors (Id, FName, LName,OfficeId ) VALUES (2, 'Yasmeen', 'Mohammed',2);
        // INSERT INTO Instructors (Id, FName, LName,OfficeId ) VALUES (3, 'Khalid', 'Hassan',3);
        // INSERT INTO Instructors (Id, FName, LName,OfficeId ) VALUES (4, 'Nadia', 'Ali',4);
        // INSERT INTO Instructors (Id, FName, LName,OfficeId ) VALUES (5, 'Omar', 'Ibrahim',5);
        return
                [
                    new Instructor { Id = 1, FName = "Ahmed", LName = "Abdullah",OfficeId=1 },
                    new Instructor { Id = 2, FName = "Yasmeen", LName = "Mohammed",OfficeId=2 },
                    new Instructor { Id = 3, FName = "Khalid", LName = "Hassan",OfficeId=3 },
                    new Instructor { Id = 4, FName = "Nadia", LName = "Ali",OfficeId=4 },
                    new Instructor { Id = 5, FName = "Omar", LName = "Ibrahim",OfficeId=5 }
                ];
    }

    // private static List<Instructor> LoadInstructorsv2()
    // {
    //     // IN SQL :
    //     // INSERT INTO Instructors (Id, FName, LName ) VALUES (1, 'Ahmed', 'Abdullah');
    //     // INSERT INTO Instructors (Id, FName, LName ) VALUES (2, 'Yasmeen', 'Mohammed');
    //     // INSERT INTO Instructors (Id, FName, LName ) VALUES (3, 'Khalid', 'Hassan');
    //     // INSERT INTO Instructors (Id, FName, LName ) VALUES (4, 'Nadia', 'Ali');
    //     // INSERT INTO Instructors (Id, FName, LName ) VALUES (5, 'Omar', 'Ibrahim');
    //     return
    //             [
    //                 new Instructor { Id = 1, FName = "Ahmed", LName = "Abdullah" },
    //                 new Instructor { Id = 2, FName = "Yasmeen", LName = "Mohammed" },
    //                 new Instructor { Id = 3, FName = "Khalid", LName = "Hassan" },
    //                 new Instructor { Id = 4, FName = "Nadia", LName = "Ali" },
    //                 new Instructor { Id = 5, FName = "Omar", LName = "Ibrahim" }
    //             ];
    // }

    // private static List<Instructor> LoadInstructorsv1()
    // {
    //     // IN SQL :
    //     // INSERT INTO Instructors (Id, Name, ) VALUES (1, 'Ahmed Abdullah');
    //     // INSERT INTO Instructors (Id, Name, ) VALUES (2, 'Yasmeen Mohammed');
    //     // INSERT INTO Instructors (Id, Name, ) VALUES (3, 'Khalid Hassan');
    //     // INSERT INTO Instructors (Id, Name, ) VALUES (4, 'Nadia Ali');
    //     // INSERT INTO Instructors (Id, Name, ) VALUES (5, 'Omar Ibrahim');
    //     return
    //             [
    //                 new Instructor { Id = 1, Name = "Ahmed Abdullah" },
    //                 new Instructor { Id = 2, Name = "Yasmeen Mohammed" },
    //                 new Instructor { Id = 3, Name = "Khalid Hassan" },
    //                 new Instructor { Id = 4, Name = "Nadia Ali" },
    //                 new Instructor { Id = 5, Name = "Omar Ibrahim" }
    //             ];
    // }

}

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {

        // LOAD THE INIT VALUES FOR THE Enrollment TABLE
        builder.HasData(LoadEnrollments());

        //GET THE TABLE NAME
        builder.ToTable("Enrollments");
    }
    private static List<Enrollment> LoadEnrollments()
    {
        // IN SQL :
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (1, 6);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (2, 6);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (3, 7);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (4, 7);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (5, 8);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (6, 8);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (7, 9);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (8, 9);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (9, 10);
        // INSERT INTO Enrollments (StudentId, SectionId) VALUES (10, 10);
        return
                [
                    new Enrollment { StudentId = 1, SectionId = 6},
                    new Enrollment { StudentId = 2, SectionId = 6},
                    new Enrollment { StudentId = 3, SectionId = 7},
                    new Enrollment { StudentId = 4, SectionId = 7},
                    new Enrollment { StudentId = 5, SectionId = 8},
                    new Enrollment { StudentId = 6, SectionId = 8},
                    new Enrollment { StudentId = 7, SectionId = 9},
                    new Enrollment { StudentId = 8, SectionId = 9},
                    new Enrollment { StudentId = 9, SectionId = 10},
                    new Enrollment { StudentId = 10, SectionId = 10}
                ];
    }

}
