using EFCreateAndDropAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCreateAndDropAPI.Data.Config;
// Participant is the base class
// Individual and Coporate are the derived classes
// by default EF Core uses TPH (Table-Per-Hierarchy) mapping strategy for the inheritance
// so it will create only one table
// 1- if we added DbSet for base class only and not for derived classes -> DbSet<Participant>
// it will create table called Participant that has the Participant class properties only
// 2- if we added 2DbSet for derived classes also -> DbSet<Individual>,DbSet<Coporate>
// it will create table called Participant that has the Participant class properties + Individual class properties + Coporate class properties + Discriminator column
// and will add discriminator column by default named as "Discriminator"
// and its value will be the class name of the derived class
// we can change the discriminator column name and its value
// by using Fluent API in OnModelCreating method -> .HasDiscriminator<string>("TYPE") .HasValue<Individual>("IND") .HasValue<Coporate>("COP")

public class ParticipantConfiguration : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : FName VARCHAR(50) NOT NULL,
        // IN SQL : LName VARCHAR(50) NOT NULL,
        builder.Property(x => x.FName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 50
        builder.Property(x => x.LName).IsRequired().HasColumnType("VARCHAR").HasMaxLength(50);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 50

        //change Discriminator name and values
        // builder.HasDiscriminator<string>("Type")
        // .HasValue<Participant>("PART")
        // .HasValue<Individual>("IND")
        // .HasValue<Coporate>("COP");
        // // set the type of discriminator column to VARCHAR with max length 4
        // builder.Property("Type").IsRequired().HasColumnType("VARCHAR").HasMaxLength(4);


        //we can use TPT
        // TPT: Table-Per-Type
        // by default EF Core uses TPH
        // to use TPT we need to add 3 tables
        // one for base class and one for each derived class
        // and them we have to stop builder.HasDiscriminator<string>("Type") line
        //we can do this by creating Configuration file for derived classes
        // or using the code below
        builder.UseTptMappingStrategy();
        // IN SQL when using TPT:
        //         CREATE TABLE Participants (
        //             Id INT PRIMARY KEY,
        //             FName VARCHAR(50) NOT NULL,
        //             LName VARCHAR(50) NOT NULL
        //         );
        //         CREATE TABLE Individuals (
        //             Id INT PRIMARY KEY,
        //             University VARCHAR(50) NOT NULL,
        //             YearOfGraduation INT NOT NULL,
        //             IsIntern BIT NOT NULL,
        //             CONSTRAINT FK_Individuals_Participants FOREIGN KEY (Id) REFERENCES Participants(Id)
        //         );
        //         CREATE TABLE Corporates (
        //             Id INT PRIMARY KEY,
        //             Company VARCHAR(50) NOT NULL,
        //             JobTitle VARCHAR(50) NOT NULL,
        //             CONSTRAINT FK_Corporates_Participants FOREIGN KEY (Id) REFERENCES Participants(Id)
        //         );



        // LOAD THE INIT VALUES FOR THE STUDENT TABLE
        // builder.HasData(LoadParticipants());

        //GET THE TABLE NAME
        builder.ToTable("Participants");
    }
    // private static List<Participant> LoadParticipants()
    // {
    //     // var part1 = new Participant { Id = 0, FName = "Omar", LName = "Youssef" };
    //     // var part2 = new Individual { Id = 1, FName = "Abdullah", LName = "Ali", University = "", YearOfGraduation = 2025, IsIntern = true };
    //     // var part3 = new Individual { Id = 2, FName = "Reem", LName = "Ahmed", University = "", YearOfGraduation = 2026, IsIntern = false };
    //     // var part4 = new Coporate { Id = 3, FName = "Noor", LName = "Saleh", Company = "Google", JobTitle = "Software Engineer" };
    //     // var part5 = new Coporate { Id = 4, FName = "Sara", LName = "Youssef", Company = "Microsoft", JobTitle = "Project Manager" };
    //     // context.Participants.AddRange(part1, part2, part3, part4, part5);
    //     return
    //             [
    //                 new Participant { Id = 1, FName = "Fatima", LName = "Ali"},
    //                 new Participant { Id = 2, FName = "Noor", LName = "Saleh"},
    //                 new Participant { Id = 3, FName = "Omar", LName = "Youssef"},
    //                 new Participant { Id = 4, FName = "Huda", LName = "Ahmed"},
    //                 new Participant { Id = 5, FName = "Amira", LName = "Tariq"},
    //                 new Participant { Id = 6, FName = "Zainab", LName = "Ismail"},
    //                 new Participant { Id = 7, FName = "Yousef", LName = "Farid"},
    //                 new Participant { Id = 8, FName = "Layla", LName = "Mustafa"},
    //                 new Participant { Id = 9, FName = "Mohammed", LName = "Adel"},
    //                 new Participant { Id = 10, FName = "Samira", LName = "Nabil"}
    //             ];
    // }

}
