using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.MovieAgg.Entities;

namespace Project.Infra.Data.Mappings
{
    public class VoteMap : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.ToTable("Votes");

            builder.Property(vote => vote.Id).ValueGeneratedOnAdd();

            builder.Property(vote => vote.Active).HasDefaultValue(true);

            builder.Property(vote => vote.Value)
                .IsRequired()
                .HasColumnType("int");

            builder.HasOne(vote => vote.Movie)
                .WithMany(a => a.Votes)
                .HasForeignKey(bc => bc.MovieId);

            builder.HasOne(vote => vote.User)
                .WithMany(a => a.Votes)
                .HasForeignKey(bc => bc.UserId);
        }
    }
}
