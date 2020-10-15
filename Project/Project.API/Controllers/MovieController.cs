using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces;
using Project.Domain.DTO.Movie;
using Project.Domain.MovieAgg;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project.API.Controllers
{
    /// <summary>
    /// Controlador dos métodos relacionados a usuário
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;
        private readonly IVoteService _voteService;

        /// <summary>
        /// Construtor do Controlador
        /// </summary>
        public MovieController(ILogger<MovieController> logger, IUserService userService, IMovieService movieService, IVoteService voteService)
        {
            _logger = logger;
            _movieService = movieService;
            _voteService = voteService;
        }

        /// <summary>
        /// Registra um novo filme no sistema. 
        /// </summary>
        /// <param name="movieRegisterDto">View com os dados do novo filme.</param>
        [HttpPost]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> Register(MovieForRegisterDto movieRegisterDto)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            _logger.LogInformation(string.Format($"{nameof(Register)} iniciado pelo usuário {userLogin}"));

            var result = await _movieService.Register(movieRegisterDto);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(Register)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }
            _logger.LogInformation(string.Format($"{nameof(Register)} finalizado."));
            return CreatedAtRoute(new { controller = "Movie", id = result.Object.Id }, result.Object.Id);
        }

        /// <summary>
        /// Registra o voto de um usuário sobre um filme no sistema. 
        /// </summary>
        /// <param name="value">Nota do usuário, deve estar entre 0 e 4.</param>
        /// <param name="id">Identificador do filme</param>
        [HttpPost("vote")]
        [Authorize(Policy = "RequireMemberRole")]
        public async Task<IActionResult> Vote([FromQuery] int value, int id)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            _logger.LogInformation(string.Format($"{nameof(Vote)} iniciado pelo usuário {userLogin}"));

            var result = await _voteService.Vote(value, id, userId);
            if (!result.Succeeded)
            {

                _logger.LogInformation(string.Format($"{nameof(Vote)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(Vote)} finalizado."));
            return Ok();
        }

        /// <summary>
        /// Retorna todos os filmes do sistema. 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> AllMovies([FromQuery] MovieFilterDto filtersDto, int pageSize = 10, int pageNumber = 1)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            _logger.LogInformation(string.Format($"{nameof(AllMovies)} iniciado pelo usuário {userLogin}"));

            var result = await _movieService.AllMovies(filtersDto, pageSize, pageNumber);

            _logger.LogInformation(string.Format($"{nameof(AllMovies)} finalizado."));
            return Ok(result);
        }

        /// <summary>
        /// Retorna o filme com o id especificado. 
        /// </summary>
        /// <param name="id">Id do filme a ser buscado.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> MovieById(int id)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            _logger.LogInformation(string.Format($"{nameof(MovieById)} iniciado pelo usuário {userLogin}"));

            var result = await _movieService.MovieById(id);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(MovieById)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(MovieById)} finalizado."));
            return Ok(result.Object);
        }
    }
}
