using Project.Domain.DTO.Movie;
using Project.Domain.MovieAgg;
using Project.Domain.Shared.Entities;
using System.Threading.Tasks;

namespace Project.Application.Interfaces
{
    public interface IMovieService
    {
        Task<Result<Movie>> Register(MovieForRegisterDto movieRegisterDto);
        Task<Result<PaginatedList<MovieForDetailedDto>>> AllMovies(MovieFilterDto filtersDto, int pageSize, int pageNumber);
        Task<Result<MovieForDetailedDto>> MovieById(int id);
    }
}
