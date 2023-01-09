using Api.Models;
using Application.Interfaces;
using Domain.Domain.Dtos.AutoComplete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AutoCompleteController : BaseController
    {
        #region Construtor
        private readonly IAutoCompleteService _autoCompleteService;
        #endregion

        #region Atributos
        public AutoCompleteController(IAutoCompleteService autoCompleteService)
        {
            _autoCompleteService = autoCompleteService;
        }
        #endregion

        #region HttpGet
        [HttpGet("AutoCompleteLogin")]
        [ProducesResponseType(typeof(StandardReturn<IEnumerable<AutoCompleteDto>>), 200)]
        [ProducesResponseType(typeof(Exception), 400)]
        [ProducesResponseType(500)]
        public Task<IActionResult> GetAutoCompleteLogin(string search)
        {
            return Task.Run(() =>
            {
                try
                {
                    return Ok(new StandardReturn<IEnumerable<AutoCompleteDto>>(ReturnStatus.Ok, _autoCompleteService.AutoCompleteLogin(search)));
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
