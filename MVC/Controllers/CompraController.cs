using Dominio.Entidades;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class CompraController : Controller
    {
        private RepositorioPrestadorSalud repoprestador = new RepositorioPrestadorSalud();
        private RepositorioVacuna repovacuna = new RepositorioVacuna();
        private RepositorioCompra repocompra = new RepositorioCompra();

        private HttpClient clienteApi = new HttpClient();
        private HttpResponseMessage respuesta = new HttpResponseMessage();

        private Uri UriBase = new Uri(@"http://localhost:50473/api/compras");

        private void ConfigurarCliente()
        {
            clienteApi.BaseAddress = UriBase;

            clienteApi.DefaultRequestHeaders.Accept.Clear();
            clienteApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Index(string nombre, string error)
        {
            TempData["Vacuna"] = nombre;
            if (Session["logueadocedula"] != null)
            {
                if(error == "error")
                {
                    ViewBag.error = "Error al realizar la compra";
                }
                ViewBag.prestadores = repoprestador.FindAll();
                return View(repovacuna.FindByNombre(nombre));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }            
        }

        [HttpPost]
        public ActionResult Index(int cantidad, string select)
        {
            PrestadorSalud prestador = new PrestadorSalud();          
            prestador = repoprestador.FindByNum(select);
            Vacuna vacuna = new Vacuna();           
            vacuna = repovacuna.FindByNombre2(TempData["Vacuna"].ToString());

            if (select == "Opciones")
            {
                return RedirectToAction("Index", new { nombre = vacuna.Nombre, error = "error" });
            }

            IEnumerable<Compra> comprasxprestador = repocompra.FindByPrestadorMes(select);
            decimal montogastado = 0;
            if(comprasxprestador.Count() > 0)
            {
                foreach (Compra compras in comprasxprestador)
                {
                    montogastado += compras.CantDosis * compras.Vacuna.Precio;
                }
            }          
            decimal saldo = prestador.CantAfiliados * prestador.MontoMaxVacunas;
            decimal nuevomontogastado = montogastado+(cantidad * vacuna.Precio);           
            try
            {
                if(nuevomontogastado <= saldo && prestador.TopeCompras > comprasxprestador.Count())
                {
                    Compra c = new Compra
                    {
                        CantDosis = cantidad,
                        SaludPrestador = select,
                        LaVacuna = vacuna.Id,
                        Fecha = DateTime.Today
                    };
                    ConfigurarCliente();
                    var accesoapi = clienteApi.PostAsJsonAsync(clienteApi.BaseAddress,c);
                    accesoapi.Wait();
                    respuesta = accesoapi.Result;
                    if (respuesta.IsSuccessStatusCode )
                    {
                        TempData["Prestador"] = select;
                        TempData["Saldo"] = saldo - nuevomontogastado;
                        return RedirectToAction("ListarCompras");
                    }
                    else
                    {
                        ModelState.AddModelError("Error Api", "No se obtuvo respuesta   " + respuesta.ReasonPhrase);
                    }
                }
                return RedirectToAction("Index", new { nombre = vacuna.Nombre, error="error" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Se produjo una excepción: ", ex.Message);
                return RedirectToAction("Index", new { nombre = vacuna.Nombre, error = "error" });
            }
        }
        
        public ActionResult ListarCompras()
        {
            PrestadorSalud prestador = repoprestador.FindByNum(TempData["Prestador"].ToString());
            IEnumerable<Compra> comprasxprestador = repocompra.FindByPrestadorMes(prestador.NumRegistro);
            if (Session["logueadocedula"] != null)
            {
                ViewBag.compras = prestador.TopeCompras - comprasxprestador.Count();
                ViewBag.autorizado = prestador.MontoMaxVacunas * prestador.CantAfiliados;
                ViewBag.disponible = TempData["Saldo"];
                return View(comprasxprestador);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }          
        }
    }
}
