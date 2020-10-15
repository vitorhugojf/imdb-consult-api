using System.Threading.Tasks;
using Project.Domain.DTO.Movie;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.Repositories;

namespace Project.Domain.MovieAgg.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<PaginatedList<MovieForDetailedDto>> GetAllPaginated(MovieFilterDto filtersDto, int pageSize, int pageNumber);
        Task<MovieForDetailedDto> GetDetailedById(int id);
    }
}
