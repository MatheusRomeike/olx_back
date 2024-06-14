using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
using Domain.Dtos.Mensagem;
using Domain.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InteresseController: BaseController
    {
        #region Construtor
        private readonly IInteresseService _interesseService;
        #endregion

        #region Atributos
        public InteresseController(
            IInteresseService interesseService)
        {
            _interesseService = interesseService;
        }
        #endregion

        #region Métodos
        [HttpPost()]
        [Authorize]
        public IActionResult Toggle([FromBody] InteresseViewModel model)
        {
            try
            {
                _interesseService.Toggle(model, UsuarioId);
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
