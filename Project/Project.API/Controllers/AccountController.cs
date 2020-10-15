using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Application.Interfaces;
using Project.Application.ViewModels.User;
using Project.Domain.UserAgg;
using System.Net;
using System.Threading.Tasks;
using Project.API.Resources;

namespace Project.API.Controllers
{
    /// <summary>
    /// Controlador dos métodos relacionados a conta
    /// </summary>
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Construtor do Controlador
        /// </summary>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, SignInManager<User> signInManager)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Login no sistema. 
        /// </summary>
        /// <param name="userLoginVm">View com o login e senha do usuário.</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginVm userLoginVm)
        {
            _logger.LogInformation(string.Format($"{nameof(Login)} iniciado pelo usuário {userLoginVm.UserName}"));

            var isValidUser = await _accountService.AuthenticateUser(userLoginVm);

            if (isValidUser != HttpStatusCode.OK)
            {
                if (isValidUser.Equals(HttpStatusCode.Locked))
                {
                    _logger.LogWarning(string.Format(Messages.UserInvalidLockedLogin));
                    return BadRequest(new { error = string.Format(Messages.UserInvalidLockedLogin) });
                }

                _logger.LogWarning(string.Format(Messages.UserInvalidToLogin, userLoginVm.UserName, userLoginVm.Password));
                return BadRequest(new { error = string.Format(Messages.UserInvalidToLogin, userLoginVm.UserName, userLoginVm.Password) });
            }

            var tokenResult = await _accountService.GenerateToken(userLoginVm);
            _logger.LogInformation(string.Format($"{nameof(Login)} finalizado"));
            return Ok(tokenResult);
        }
    }
}
