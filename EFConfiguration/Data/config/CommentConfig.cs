using EFConfiguration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EFConfiguration.Data.Config
{
    internal class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("tblComments");
            builder.HasKey(x => x.CommentId);
            builder.Property(x => x.CommentId).UseIdentityColumn(1, 1);
            builder.Property(x => x.CommentText).IsRequired().HasMaxLength(100);
        }
    }
}