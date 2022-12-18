using Microsoft.AspNetCore.Mvc;
using Api.Models;

namespace Api.Controllers
{
    public class BaseController : ControllerBase
    {
        #region Atributos
        /// <summary>
        /// Id do úsuario logado
        /// </summary>
        public int LoginId => Convert.ToInt16(HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "LoginId")?.Value);
        #endregion

        #region Métodos
        /// <summary>
        /// Método responsável por resolver o resultado de erro.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected IActionResult ResolveError(Exception e)
        {
            return BadRequest(new StandardReturn<IEnumerable<string>>(ReturnStatus.Error, new List<string> { e.Message }));
        }
        #endregion
    }
}
