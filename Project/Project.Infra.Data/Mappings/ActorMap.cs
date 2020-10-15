using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.MovieAgg.Entities;

namespace Project.Infra.Data.Mappings
{
    public class ActorMap : IEntityTypeConfiguration<Actor>
    {
        public void Configure(EntityTypeBuilder<Actor> builder)
        {
            builder.ToTable("Actors");

            builder.Property(actor => actor.Id).ValueGeneratedOnAdd();

            builder.Property(actor => actor.Active).HasDefaultValue(true);

            builder.Property(actor => actor.Name)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);
        }
    }
}
