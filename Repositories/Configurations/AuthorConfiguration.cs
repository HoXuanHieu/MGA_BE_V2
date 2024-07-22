using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace Repositories.Configurations
{
    internal class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
    {
        public void Configure(EntityTypeBuilder<AuthorEntity> builder)
        {
            builder.HasKey(x => x.AuthorId);
            builder.Property(x => x.AuthorName).IsRequired().HasMaxLength(70);
            builder.Property(x => x.Description).HasDefaultValue("description");
            builder.Property(x => x.DateCreate).IsRequired().HasDefaultValue(DateTime.Now);
            builder.Property(x => x.DateUpdate).IsRequired();
            builder.Property(x => x.LastActivity).HasDefaultValue("");
        }
    }
}
