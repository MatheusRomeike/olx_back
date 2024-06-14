﻿using Application.Interfaces;
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
        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromForm] AnuncioViewModel model)
        {
            try
            {
                model.UsuarioId = UsuarioId;
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


        [HttpGet("List")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<AnuncioDto>), 200)]
        public async Task<IActionResult> List([FromQuery] FiltrarAnuncioViewModel model)
        {
            try
            {
                var anuncios = await _anuncioService.List(model);
                return Ok(anuncios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTituloAnuncio")]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<AnuncioDto>), 200)]
        public async Task<IActionResult> GetTituloAnuncio(int anuncioId)
        {
            try
            {
                var anuncios = _anuncioService.GetTituloAnuncio(anuncioId);
                return Ok(anuncios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("LoadById")]
        [Authorize]
        [ProducesResponseType(typeof(AnuncioDto), 200)]
        public async Task<IActionResult> LoadById(int anuncioId)
        {
            try
            {
                var anuncio = await _anuncioService.LoadByIdAsync(anuncioId, UsuarioId);
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
        #endregion

    }
}
