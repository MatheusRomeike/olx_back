using Api.Models;
using Application.Interfaces;
using Domain.AutoComplete;
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

        #endregion
    }
}
