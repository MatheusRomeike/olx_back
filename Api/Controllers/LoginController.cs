using Api.Models;
using Application.Interfaces;
using Domain.Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class LoginController : BaseController
    {
        #region Atributos
        private readonly ILoginService _loginService;
        #endregion

        #region Construtor
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        #endregion

        #region HttpGet
        [HttpGet("VerificarLogin")]
        [ProducesResponseType(typeof(StandardReturn<bool>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> VerificarLogin()
        {
            return Task.Run(() =>
            {
                try
                {
                    return Ok(new StandardReturn<bool>(ReturnStatus.Ok, true));
                }
                catch (Exception e)
                {
                    return ResolveError(e);
                }
            });
        }
        #endregion

        #region HttpPost
        [AllowAnonymous]
        [HttpPost("AdicionarLogin")]
        [ProducesResponseType(typeof(StandardReturn<bool>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> AdicionarLogin([FromBody] LoginViewModel login)
        {
            return Task.Run(() =>
            {
                try
                {
                    return Ok(new StandardReturn<bool>(ReturnStatus.Ok, _loginService.AdicionarLogin(login)));
                }
                catch (Exception e)
                {
                    return ResolveError(e);
                }
            });
        }

        [AllowAnonymous]
        [HttpPost("Logar")]
        [ProducesResponseType(typeof(StandardReturn<string>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> Logar([FromBody] LoginViewModel login)
        {
            return Task.Run(() =>
            {
                try
                {
                    return Ok(new StandardReturn<string>(ReturnStatus.Ok, _loginService.Logar(login)));
                }
                catch (Exception e)
                {
                    return ResolveError(e);
                }
            });
        }


        #endregion
    }
}
