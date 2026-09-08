using EFRawSQLQuery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFRawSQLQuery.Data.Config
{
    public class CourseOverviewConfiguration : IEntityTypeConfiguration<CourseOverview>
    {
        public void Configure(EntityTypeBuilder<CourseOverview> builder)
        {
            builder.HasNoKey();
            builder.ToView("CourseOverview");
        }
    }
}
