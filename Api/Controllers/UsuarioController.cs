using Application.Interfaces;
using Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuarioController : BaseController
    {
        #region Construtor
        private readonly IUsuarioService _usuarioService;
        #endregion

        #region Atributos
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        #endregion

        #region HttpGet
        [HttpGet]
        public async Task<IActionResult> ObterAsync()
        {
            try
            {
                var result = await _usuarioService.ObterAsync(UsuarioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region HttpPost
        [HttpPost("Add")]
        [AllowAnonymous]
        public IActionResult Add([FromBody] UsuarioViewModel usuario)
        {
            try
            {
                var result = _usuarioService.AdicionarUsuario(usuario);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Logar([FromBody] LoginViewModel usuario)
        {
            try
            {
                var token = _usuarioService.Logar(usuario);
                return Ok(token);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region HttpPatch
        [HttpPatch]
        public async Task<IActionResult> AtualizarAsync([FromForm] UsuarioAtualizarViewModel usuario)
        {
            try
            {
                var result = await _usuarioService.AtualizarUsuarioAsync(usuario, UsuarioId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
