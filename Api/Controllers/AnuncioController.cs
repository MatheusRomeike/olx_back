using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Dtos.Anuncio;
using Domain.Dtos.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnuncioController : BaseController
    {
        #region Construtor
        private readonly IAnuncioService _anuncioService;
        #endregion

        #region Atributos
        public AnuncioController(
            IAnuncioService anuncioService)
        {
            _anuncioService = anuncioService;
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método responsável por inserir um anúncio.
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromForm] AnuncioViewModel anuncio)
        {
            try
            {
                anuncio.UsuarioId = UsuarioId;
                var id = _anuncioService.Add(anuncio);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por atualizar um anúncio.
        /// </summary>
        /// <param name="anuncio"></param>
        /// <returns></returns>
        [HttpPatch("Update")]
        [Authorize]
        public IActionResult Update([FromBody] AnuncioViewModel anuncio)
        {
            try
            {
                _anuncioService.Update(anuncio);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por carregar um anúncio a partir do seu Id.
        /// </summary>
        /// <param name="anuncioId"></param>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        [HttpGet("LoadById")]
        [Authorize]
        [ProducesResponseType(typeof(AnuncioDto), 200)]
        public async Task<IActionResult> LoadById(int anuncioId, int usuarioId)
        {
            try
            {
                var anuncio =  await _anuncioService.LoadByIdAsync(anuncioId, usuarioId == 0  ? UsuarioId : usuarioId);
                return Ok(anuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Método responsável por deletar um anúncio a partir do seu Id.
        /// </summary>
        /// <param name="anuncioId"></param>
        /// <returns></returns>
        [HttpDelete("Delete")]
        [Authorize]
        public IActionResult Delete([FromBody] int anuncioId)
        {
            try
            {
                _anuncioService.Delete(anuncioId, UsuarioId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por obter relatório de vendas dos anúncios do usuário logado.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet("RelatorioVendasAnuncio")]
        [ProducesResponseType(typeof(RelatorioVendasDto), 200)]
        public IActionResult RelatorioVendasAnuncio([FromQuery] RelatorioVendasViewModel model)
        {
            try
            {

                return Ok(_anuncioService.RelatorioVendasAnuncio(model, UsuarioId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por carregar todos os anúncios do usuário logado.
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoadByUsuario")]
        [Authorize]
        public IActionResult LoadByUsuario()
        {
            try
            {
                var anuncio = _anuncioService.LoadByUsuario(UsuarioId);
                return Ok(anuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Método responsável por carregar todas as categorias cadastradas.
        /// </summary>
        /// <returns></returns>
        [HttpGet("LoadCategorias")]
        [Authorize]
        public IActionResult LoadCategorias()
        {
            try
            {
                var anuncio = _anuncioService.LoadCategorias();
                return Ok(anuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método responsável por inserir as fotos do anúncio.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddFotos")]
        [Authorize]
        public IActionResult AddFotos([FromForm] AnuncioViewModel model)
        {
            try
            {
                model.UsuarioId = UsuarioId;
                _anuncioService.InserirFotoAsync(model);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

    }
}
