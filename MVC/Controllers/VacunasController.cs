using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class VacunasController : Controller
    {
        private HttpClient clienteApi = new HttpClient();
        private HttpResponseMessage respuesta = new HttpResponseMessage();

        private Uri UriBase = new Uri(@"http://localhost:50473/api/vacunas");

        private void ConfigurarCliente()
        {
            clienteApi.BaseAddress = UriBase;

            clienteApi.DefaultRequestHeaders.Accept.Clear();
            clienteApi.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ActionResult Filter()
        {
            if (Session["logueadocedula"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }           
        }

        [HttpPost]
        public ActionResult Filter(int? fase, int? rangomin, int? rangomax, string tipo, string laboratorio, string pais, string select)
        {
            try
            {
                if ((rangomax == null && rangomin != null) || (rangomax != null && rangomin == null))
                {
                    ViewBag.error = "Debe cargar ambos rangos de precio";
                    return View();
                }
                if (select == "interseccion")
                {
                    ConfigurarCliente();
                    respuesta = clienteApi.GetAsync(clienteApi.BaseAddress + "/" + "filtradas?fase=" + fase + "&rangomin=" + rangomin + "&rangomax=" + rangomax + "&tipo=" + tipo + "&laboratorio=" + laboratorio + "&pais=" + pais).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var contenido = respuesta.Content.ReadAsAsync<IEnumerable<Vacuna>>().Result;
                        if (contenido != null)
                        {
                            ViewBag.lista = contenido;
                        }
                    }
                    else
                    {
                        ViewBag.error = "No se obtuvo respuesta";
                        return View();
                    }
                }
                else if(select == "union")
                {
                    ConfigurarCliente();
                    respuesta = clienteApi.GetAsync(clienteApi.BaseAddress + "/" + "filtradasunion?fase=" + fase + "&rangomin=" + rangomin + "&rangomax=" + rangomax + "&tipo=" + tipo + "&laboratorio=" + laboratorio + "&pais=" + pais).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var contenido = respuesta.Content.ReadAsAsync<IEnumerable<Vacuna>>().Result;
                        if (contenido != null)
                        {
                            ViewBag.lista = contenido;
                        }
                    }
                    else
                    {
                        ViewBag.error = "No se obtuvo respuesta";
                        return View();
                    }
                }
                else
                {
                    ConfigurarCliente();
                    respuesta = clienteApi.GetAsync(clienteApi.BaseAddress + "/" + "filtradas?fase=" + null + "&rangomin=" + null + "&rangomax=" + null + "&tipo=" + null + "&laboratorio=" + null + "&pais=" + null).Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var contenido = respuesta.Content.ReadAsAsync<IEnumerable<Vacuna>>().Result;
                        if (contenido != null)
                        {
                            ViewBag.lista = contenido;
                        }
                    }
                    else
                    {
                        ViewBag.error = "No se obtuvo respuesta";
                        return View();
                    }
                }
                return View();
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("Se produjo una excepción: ", ex.Message);
                return View();
            }
        }
    }
}
