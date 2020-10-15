using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.UserAgg;

namespace Project.Infra.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.Property(user => user.Id).ValueGeneratedOnAdd();

            builder.Property(user => user.Active)
                .IsRequired();

            builder.Property(user => user.FirstName)
                .IsRequired(false)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(user => user.LastName)
                .IsRequired(false)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(user => user.Introduction)
                .IsRequired(false)
                .HasColumnType("varchar(1000)")
                .HasMaxLength(1000);

            builder.Property(user => user.Created)
                .HasColumnType("datetime");

            builder.Property(user => user.Updated)
                .HasColumnType("datetime");
        }
    }
}
