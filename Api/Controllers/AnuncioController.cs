using Application.Interfaces;
using Application.Services;
using Application.ViewModels;
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
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromBody] AnuncioViewModel model)
        {
            try
            {
                _anuncioService.Add(model);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("Update")]
        [Authorize]
        public IActionResult Update([FromBody] AnuncioViewModel model)
        {
            try
            {
                _anuncioService.Update(model);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("LoadById")]
        [Authorize]
        public IActionResult LoadById(int anuncioId)
        {
            try
            {
                var anuncio = _anuncioService.LoadById(anuncioId);
                return Ok(anuncio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Delete")]
        [Authorize]
        public IActionResult Delete(int anuncioId)
        {
            try
            {
                _anuncioService.Delete(anuncioId);
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
