using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.UserAgg.Entities;

namespace Project.Infra.Data.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique(false);
            
            builder.Property(role => role.Id).ValueGeneratedOnAdd();
        }
    }
}
