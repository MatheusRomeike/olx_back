using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Anuncio;
using Domain.Dtos.Anuncio;
using Domain.Dtos.Mensagem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MensagemController : BaseController
    {
        #region Construtor
        private readonly IMensagemService _mensagemService;
        #endregion

        #region Atributos
        public MensagemController(
            IMensagemService mensagemService)
        {
            _mensagemService = mensagemService;
        }
        #endregion
        
        #region Métodos
        [HttpGet("Chat")]
        [Authorize]
        [ProducesResponseType(typeof(MensagemDto), 200)]
        public IActionResult List(int anuncioId, int usuarioInteressadoId)
        {
            try
            {
                return Ok(_mensagemService.List(anuncioId, usuarioInteressadoId, UsuarioId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
