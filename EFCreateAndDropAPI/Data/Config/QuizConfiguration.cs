using EFCreateAndDropAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCreateAndDropAPI.Data.Config;
//TPC means Table-Per-Concrete Class.
//IT Creates a Separate Table for Each sub Class not for abstract class
//To use TPC strategy we need to create Configuration file for base class
//and call builder.UseTpcMappingStrategy() in base configuration file
//also we need to create Configuration file for derived classes
public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
{
    public void Configure(EntityTypeBuilder<Quiz> builder)
    {
        //IN SQL : Id INT PRIMARY KEY,
        builder.HasKey(x => x.Id);// set the primary key
        builder.Property(x => x.Id).ValueGeneratedNever();// set the primary key value to generated manually

        // IN SQL : Title VARCHAR(50) NOT NULL,
        builder.Property(x => x.Title).IsRequired().HasColumnType("VARCHAR").HasMaxLength(255);//SET ITS TYPE as VARCHAR AND MAX LENGTH as 50

        //we can use TPC
        builder.UseTpcMappingStrategy();

    }
}
