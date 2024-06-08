using Application.Interfaces;
using Application.ViewModels;
using Domain.Dtos.Anuncio;
using Domain.Dtos.Autenticacao;
using Domain.Dtos.Usuario;
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
        /// <summary>
        /// Método responsável por obter os dados do usuário logado.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UsuarioDto), 200)]
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
        /// <summary>
        /// Método responsável por adicionar um novo usuário.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [ProducesResponseType(typeof(bool), 200)]
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

        /// <summary>
        /// Método responsável por logar um usuário.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenDto), 200)]
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
        /// <summary>
        /// Método responsável por atualizar os dados do usuário logado.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPatch]
        [ProducesResponseType(typeof(bool), 200)]
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
