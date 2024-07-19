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
    internal class MangaConfiguration : IEntityTypeConfiguration<MangaEntity>
    {
        public void Configure(EntityTypeBuilder<MangaEntity> builder)
        {
            builder.HasKey(x => x.MangaId);
            builder.Property(x => x.MangaName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MangaImage).IsRequired().HasMaxLength(100);
            builder.Property(x => x.DateCreated).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.Categories).IsRequired().HasMaxLength(255);
            builder.Property(x => x.DateUpdated).IsRequired();
            builder.Property(x => x.IsApproval).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsDelete).IsRequired().HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.MangaPost).HasForeignKey(x => x.PostedBy);
            builder.HasOne(x => x.Author).WithMany(x => x.Mangas).HasForeignKey(x => x.AuthorId); 
        }
    }
}
