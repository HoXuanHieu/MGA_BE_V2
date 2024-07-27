using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using Models.Entities;

namespace Repositories.Configurations
{
    public class ChapterImageConfiguration : IEntityTypeConfiguration<ChapterImageEntity>
    {
        public void Configure(EntityTypeBuilder<ChapterImageEntity> builder)
        {
            builder.HasKey(x => x.ChapterImageId);
            builder.Property(x => x.LastActivity).HasDefaultValue("");
            builder.Property(x => x.ChapterName).IsRequired().HasMaxLength(70);
            builder.Property(x => x.DateCreate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ChapterImagePath).IsRequired().HasDefaultValue("");
            builder.HasOne(x => x.Chapter).WithMany(x => x.ChapterImage).HasForeignKey(x => x.ChapterId);
        }
    }
}
