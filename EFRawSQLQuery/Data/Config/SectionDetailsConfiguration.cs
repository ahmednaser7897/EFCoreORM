using EFRawSQLQuery.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFRawSQLQuery.Data.Config
{
    public class SectionDetailsConfiguration : IEntityTypeConfiguration<SectionDetails>
    {
        public void Configure(EntityTypeBuilder<SectionDetails> builder)
        {
            builder.HasNoKey();
        }
    }
}
