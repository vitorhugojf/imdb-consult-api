using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces;
using Project.Domain.DTO.User;
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
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        /// <summary>
        /// Construtor do Controlador
        /// </summary>
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        /// <summary>
        /// Registra um novo usuário no sistema. 
        /// </summary>
        /// <param name="userRegisterDto">View com os dados do novo usuário.</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserForRegisterDto userRegisterDto)
        {
            _logger.LogInformation(string.Format($"{nameof(Register)} iniciado para o usuário {userRegisterDto.Email}"));

            var result = await _userService.Register(userRegisterDto);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(Register)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(Register)} finalizado."));
            return CreatedAtRoute(new { controller = "User", id = result.Object.Id }, result.Object.Id);
        }

        /// <summary>
        /// Retorna todos os usuários do sistema. 
        /// </summary>
        [HttpGet]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> AllUsers([FromQuery] int pageSize = 10, int pageNumber = 1, bool orderAsc = true)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            _logger.LogInformation(string.Format($"{nameof(AllUsers)} iniciado pelo usuário {userLogin}"));

            var result = await _userService.AllUsers(pageSize, pageNumber, orderAsc);

            _logger.LogInformation(string.Format($"{nameof(AllUsers)} finalizado."));
            return Ok(result.Object);
        }

        /// <summary>
        /// Retorna o usuário com o id especificado. 
        /// </summary>
        /// <param name="id">Id do usuário a ser buscado.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> UserById(int id)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;
            _logger.LogInformation(string.Format($"{nameof(UserById)} iniciado pelo usuário {userLogin}"));

            var result = await _userService.UserById(id);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(UserById)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(UserById)} finalizado."));
            return Ok(result.Object);
        }

        /// <summary>
        /// Atualiza um usuário no sistema.
        /// </summary>
        /// <param name="userForUpdateDto">View com os dados do usuário a ser atualizado.</param>
        [HttpPut]
        public async Task<IActionResult> Update(UserForUpdateDto userForUpdateDto)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;

            _logger.LogInformation(string.Format($"{nameof(Update)} iniciado pelo usuário {userLogin}"));
            var isAdmin = User.FindAll(ClaimTypes.Role).Any(r => r.Value.Equals("Admin"));

            var userRequestId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _userService.Update(userRequestId, isAdmin, userForUpdateDto);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(Update)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { errors = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(Update)} finalizado."));
            return NoContent();
        }

        /// <summary>
        /// Desabilita o usuário no sistema.
        /// </summary>
        /// <param name="id">Identificador do usuário a ser desabilitado.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Disable(int id)
        {
            var userLogin = User.FindFirst(ClaimTypes.Email).Value;

            _logger.LogInformation(string.Format($"{nameof(Disable)} iniciado pelo usuário {userLogin}"));
            var isAdmin = User.FindAll(ClaimTypes.Role).Any(r => r.Value.Equals("Admin"));

            var userRequestId = int.Parse(User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _userService.Disable(userRequestId, isAdmin, id);
            if (!result.Succeeded)
            {
                _logger.LogInformation(string.Format($"{nameof(Disable)} falhou, motivo: {result.Errors.First()}"));
                return BadRequest(new { error = result.Errors });
            }

            _logger.LogInformation(string.Format($"{nameof(Disable)} finalizado."));
            return NoContent();
        }
    }
}
