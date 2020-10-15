using AutoMapper;
using Project.Application.Interfaces;
using Project.Domain.DTO.Movie;
using Project.Domain.MovieAgg;
using Project.Domain.MovieAgg.Repositories;
using Project.Domain.Shared.Entities;
using Project.Domain.Shared.Interfaces.UoW;
using Project.Domain.UserAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        private readonly IUnityOfWork _unityOfWork;

        public MovieService(IMovieRepository movieRepository, IMapper mapper, IUnityOfWork unityOfWork)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
            _unityOfWork = unityOfWork;
        }
        public async Task<Result<Movie>> Register(MovieForRegisterDto movieRegisterDto)
        {
            var movieToCreate = _mapper.Map<Movie>(movieRegisterDto);

            await _movieRepository.Create(movieToCreate);
            var commit = _unityOfWork.Commit();

            return commit ?
                Result<Movie>.CreateResult(movieToCreate) :
                Result<Movie>.CreateResult(movieToCreate, new HashSet<string> { "Erro ao registrar usuário." });
        }

        public async Task<Result<PaginatedList<MovieForDetailedDto>>> AllMovies(MovieFilterDto filtersDto, int pageSize, int pageNumber)
        {
            var result = await _movieRepository.GetAllPaginated(filtersDto, pageSize, pageNumber);
            return Result<PaginatedList<MovieForDetailedDto>>.CreateResult(result);
        }

        public async Task<Result<MovieForDetailedDto>> MovieById(int id)
        {
            var movie = await _movieRepository.GetDetailedById(id);
            return Result<MovieForDetailedDto>.CreateResult(movie);
        }
    }
}
