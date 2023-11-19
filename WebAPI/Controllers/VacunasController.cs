using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repositorios;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/vacunas")]
    public class VacunasController : ApiController
    {
        RepositorioVacuna repovacuna = new RepositorioVacuna();

        [Route("filtradas")]
        [HttpGet]
        public IHttpActionResult FiltrarVacunas(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais)
        {
            try
            {
                if(fase == null && rangomin == null && rangomax == null && tipo == null && laboratorio == null && pais == null)
                {
                    var vacunas = repovacuna.FindAll();
                    return Ok(vacunas);
                }
                else
                {
                    var vacunas = repovacuna.FiltrarVacunas(fase, rangomin, rangomax, tipo, laboratorio, pais);
                    return Ok(vacunas);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError
                (new Exception("No es posible filtrar las vacunas",
                ex.InnerException));
            }
        }

        [Route("filtradasunion")]
        [HttpGet]
        public IHttpActionResult FiltrarVacunasUnion(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais)
        {
            try
            {
                if (fase == null && rangomin == null && rangomax == null && tipo == null && laboratorio == null && pais == null)
                {
                    var vacunas = repovacuna.FindAll();
                    return Ok(vacunas);
                }
                else
                {
                    var vacunas = repovacuna.FiltrarVacunasUnion(fase, rangomin, rangomax, tipo, laboratorio, pais);
                    return Ok(vacunas);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError
                (new Exception("No es posible filtrar las vacunas",
                ex.InnerException));
            }
        }
    }
}
