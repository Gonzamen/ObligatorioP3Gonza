using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ImportacionController : Controller
    {
        RepositorioVacuna repovacuna = new RepositorioVacuna();
        public ActionResult Index()
        {
            List<string[]> listavacunas = new List<string[]>();
            List<string[]> listausuarios = new List<string[]>();
            string errores = ImportarTodos();
            List<string> lista2 = ImportarVacunas();
            List<string> lista3 = ImportarUsuarios();
            foreach (string error in lista2)
            {
                if (error.Contains("ERROR"))
                {
                    errores += error + "\n\r";
                }
                else
                {
                    string[] vecDatos = error.Split("|".ToCharArray());
                    listavacunas.Add(vecDatos);
                }
            }
            foreach (string error in lista3)
            {
                if (error.Contains("ERROR"))
                {
                    errores += error + "\n\r";
                }
                else
                {
                    string[] vecDatos = error.Split("|".ToCharArray());
                    listausuarios.Add(vecDatos);
                }
            }
            ViewBag.errores = errores;
            ViewBag.listavacunas = listavacunas;
            ViewBag.listausuarios = listausuarios;
            TempData["VacunasImportadas"] = listavacunas;
            return View();
        }

        public ActionResult Details(string nombre)
        {
            return View(repovacuna.FindByNombre(nombre));
        }

        public ActionResult VacunasImportadas()
        {
            ViewBag.lista = TempData["VacunasImportadas"];
            return View();
        }

        private string ImportarTodos()
        {
            string errores = "";     
            errores += ManejoArchivos.GenerarTiposVacuna();
            errores += ManejoArchivos.GenerarLaboratorios();
            return errores;
        }

        private List<string> ImportarVacunas()
        {
            List<string> lista = new List<string>();
            lista = ManejoArchivos.GenerarVacunas();
            return lista;
        }

        private List<string> ImportarUsuarios()
        {
            List<string> lista = new List<string>();
            lista = ManejoArchivos.GenerarUsuarios();
            return lista;
        }



    }
}
