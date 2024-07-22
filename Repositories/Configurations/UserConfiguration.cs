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
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.Property(x => x.IsVerify).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsDelete).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsSuspension).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.IsVerify).IsRequired().HasDefaultValue(false);
            builder.Property(x => x.LastActivity).HasDefaultValue("");
        }
    }
}
