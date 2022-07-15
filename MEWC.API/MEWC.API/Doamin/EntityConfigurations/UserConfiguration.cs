using MEWC.API.Doamin.Application;
using MEWC.API.Doamin.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MEWC.API.Doamin.EntityConfigurations
{
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("SEC_USER");

            builder.HasKey(x => x.user_ID);
            builder.Property(x => x.user_ID)
                .HasColumnName("USER_ID").IsRequired().HasColumnType("int").ValueGeneratedOnAdd();

            builder.Property(x => x.UserName)
                .HasColumnName("USER_NAME")
                .IsRequired()
                .HasColumnType("nvarchar(100)").HasMaxLength(100); 

            builder.Property(x => x.Password)
                .HasColumnName("PASSWORD")
                .IsRequired()
                .HasColumnType("nvarchar(MAX)");

            builder.Property(x => x.FirstName)
                .HasColumnName("FIRST_NAME")
                .IsRequired()
                .HasColumnType("nvarchar(100)").HasMaxLength(100); 

            builder.Property(x => x.LastName)
                .HasColumnName("LAST_NAME")
                .IsRequired()
                .HasColumnType("nvarchar(100)").HasMaxLength(100); 

            builder.Property(x => x.DisplayName)
                .HasColumnName("DISPLAY_NAME")
                .IsRequired()
                .HasColumnType("nvarchar(100)").HasMaxLength(100); 

            builder.Property(x => x.PasswordSalt)
                .HasColumnName("DISPLAY_NAME")
                .IsRequired()
                .HasColumnType("nvarchar(100)").HasMaxLength(100);

            builder.Property(x => x.CreatedDate)
                .HasColumnName("CREATED_DATE")
                .IsRequired()
                .HasColumnType("datetimeoffset(7)");

            builder.Property(x => x.CreatedBy)
               .HasColumnName("CREATED_BY")
               .IsRequired()
               .HasColumnType("nvarchar(100)").HasMaxLength(100);
        }
    }
}