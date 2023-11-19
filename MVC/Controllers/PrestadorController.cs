using Dominio.Entidades;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PrestadorController : Controller
    {
        RepositorioPrestadorSalud repoPrestador = new RepositorioPrestadorSalud();

        public ActionResult Register()
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
        public ActionResult Register(PrestadorSalud prestador)
        {
            int saldo = prestador.CantAfiliados * prestador.MontoMaxVacunas;
            PrestadorSalud nuevo = new PrestadorSalud
            {
                NumRegistro = prestador.NumRegistro,
                Nombre = prestador.Nombre,
                Telefono = prestador.Telefono,
                NombreContacto = prestador.NombreContacto,
                TopeCompras = prestador.TopeCompras,
                CantAfiliados = prestador.CantAfiliados,
                MontoMaxVacunas = prestador.MontoMaxVacunas,
            };
            if (prestador.Validar())
            {
                if (repoPrestador.Add(nuevo))
                {
                    ViewBag.registro = "Prestador de Salud registrado con éxito!";
                }
                else
                {
                    ViewBag.registro = "Error al agregar el Prestador de Salud a la base de datos.";
                }
            }
            else
            {
                ViewBag.registro = "Error al registrar prestador, alguno de los datos no es válido";
            }
            return View();
        }
    }
}
