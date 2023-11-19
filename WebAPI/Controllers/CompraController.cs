using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio.Entidades;
using Repositorios;

namespace WebAPI.Controllers
{
    [RoutePrefix("api/compras")]
    public class CompraController : ApiController
    {
        RepositorioCompra repocompra = new RepositorioCompra();
        RepositorioPrestadorSalud repoprestador = new RepositorioPrestadorSalud();

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var comp = repocompra.FindById(id);
            return Ok(comp);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] Compra c)
        {
            repocompra.Add(c);
            return Created("~/api/compras/" + c.Id, c);
        }

    }
}
