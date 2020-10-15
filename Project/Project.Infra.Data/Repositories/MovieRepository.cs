using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.DTO.Actor;
using Project.Domain.DTO.Movie;
using Project.Domain.MovieAgg;
using Project.Domain.MovieAgg.Entities;
using Project.Domain.MovieAgg.Repositories;
using Project.Domain.Shared.Entities;
using Project.Infra.Data.Context;

namespace Project.Infra.Data.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<PaginatedList<MovieForDetailedDto>> GetAllPaginated(MovieFilterDto filtersDto, int pageSize, int pageNumber)
        {
            var query = DbContext.Movies.AsNoTracking();

            if (!string.IsNullOrEmpty(filtersDto.Director))
            {
                query = query.Where(m => m.Director.ToLower().Contains(filtersDto.Director.ToLower()));
            }

            if (!string.IsNullOrEmpty(filtersDto.Genre))
            {
                query = query.Where(m => m.Genre.ToLower().Contains(filtersDto.Genre.ToLower()));
            }

            if (!string.IsNullOrEmpty(filtersDto.Name))
            {
                query = query.Where(m => m.Name.ToLower().Contains(filtersDto.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(filtersDto.Actor))
            {
                query = query.Where(m =>
                    m.MovieActors.Any(ma => ma.Actor.Name.ToLower().Contains(filtersDto.Actor.ToLower())));
            }

            query = query.OrderByDescending(m => (decimal)m.Votes.Sum(v => v.Value) / m.Votes.Count).ThenBy(m => m.Name);

            var result = query.Select(m => new MovieForDetailedDto
            {
                Director = m.Director,
                Genre = m.Genre,
                Name = m.Name,
                Actors = m.MovieActors.Select(ma => new ActorForDetailedDto
                {
                    Name = ma.Actor.Name
                }),
                AverageVote = (decimal)m.Votes.Sum(v => v.Value) / m.Votes.Count
            });

            return await new PaginatedList<MovieForDetailedDto>().CreateAsync(result, pageNumber, pageSize);
        }

        public async Task<MovieForDetailedDto> GetDetailedById(int id)
        {
            var query = DbContext.Movies.AsNoTracking();

            var movie = query.Where(x => x.Id == id)
                .Select(m => new MovieForDetailedDto
                {
                    Director = m.Director,
                    Genre = m.Genre,
                    Name = m.Name,
                    Actors = m.MovieActors.Select(ma => new ActorForDetailedDto
                    {
                        Name = ma.Actor.Name
                    }),
                    AverageVote = (decimal)m.Votes.Sum(v => v.Value) / m.Votes.Count
                }).FirstOrDefault();

            return movie;
        }
    }
}
