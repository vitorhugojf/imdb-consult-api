using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.MovieAgg.Entities;

namespace Project.Infra.Data.Mappings
{
    public class MovieActorMap : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.ToTable("MovieActor");

            builder.HasKey(ma => new {ma.ActorId, ma.MovieId});

            builder.HasOne(ma => ma.Actor)
                .WithMany(a => a.ActorMovies)
                .HasForeignKey(bc => bc.ActorId);

            builder.HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(bc => bc.MovieId);
        }
    }
}
