using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.MovieAgg;
using Project.Domain.MovieAgg.Entities;

namespace Project.Infra.Data.Mappings
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.Property(movie => movie.Id).ValueGeneratedOnAdd();

            builder.Property(movie => movie.Active).HasDefaultValue(true);

            builder.Property(movie => movie.Name)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(movie => movie.Director)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(movie => movie.Genre)
                .IsRequired()
                .HasColumnType("varchar(50)")
                .HasMaxLength(50);
        }
    }
}
