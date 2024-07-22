using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Configurations
{
    internal class ChapterConfiguration : IEntityTypeConfiguration<ChapterEntity>
    {
        public void Configure(EntityTypeBuilder<ChapterEntity> builder)
        {
            builder.HasKey(x => x.ChapterId);
            builder.Property(x => x.ChapterName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DateCreated).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.ChapterImage).IsRequired().HasDefaultValue("");
            builder.Property(x => x.LastActivity).HasDefaultValue("");
            builder.HasOne(x => x.manga).WithMany(x => x.Chapters).HasForeignKey(x => x.MangaId);
        }
    }
}
