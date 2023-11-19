using Dominio.Entidades;
using MVC.Models;
using Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class UsuarioController : Controller
    {
        private RepositorioUsuario repoUsuarios = new RepositorioUsuario();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Usuario usu)
        {
            if (usu.Validar())
            {
                Usuario usuario = repoUsuarios.FindByCedula(usu.Cedula);
                usu.PrimeraVez = true;
                usu.Password = Seguridad.Encriptar(usu.Password);
                if (usuario != null)
                {
                    ViewBag.msgregister = "Usuario ya registrado, inicie sesión";
                }
                else
                {
                    if (!repoUsuarios.Add(usu))
                    {
                        ViewBag.msgregister = "Credenciales de acceso incorrectas";
                    }
                    else
                    {
                        ViewBag.msgregister = "Usuario registrado correctamente!";

                    }
                }
            }
            else
            {
                ViewBag.msgregister = "Error al registrar usuario";
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario usu)
        {
            Usuario usuario = repoUsuarios.FindByCedula(usu.Cedula);
            if (usuario != null && !usuario.PrimeraVez)
            {
                Session["logueadocedula"] = usu.Cedula;
                return RedirectToAction("NewPassword");
            }
            if (usuario != null)
            {
                if (Seguridad.DesEncriptar(usuario.Password) == usu.Password)
                {
                    Session["logueadocedula"] = usuario.Cedula;
                    return RedirectToAction("Index", "Home");
                }
            }
            if (usuario == null || usu.Password != Seguridad.DesEncriptar(usuario.Password))
            {
                ViewBag.msglogin = "Credenciales incorrectas";
            }
            return View();
        }

        public ActionResult Exit()
        {
            Session["logueadocedula"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult NewPassword()
        {
            if (Session["logueadocedula"] != null)
            {
                UsuarioModel model = new UsuarioModel();
                RepositorioUsuario repo = new RepositorioUsuario();
                model = MapearUsu(repo.FindByCedula((string)Session["logueadocedula"]));
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult NewPassword(UsuarioModel unUsuario)
        {
            Usuario usuario = repoUsuarios.FindByCedula(unUsuario.Cedula);
            if (unUsuario != null && usuario.Password == unUsuario.OldPassword)
            {
                if (unUsuario.NewPassword == unUsuario.ConfirmPassword)
                {
                    Usuario nuevo = new Usuario
                    {
                        Cedula = unUsuario.Cedula,
                        Password = Seguridad.Encriptar(unUsuario.ConfirmPassword),
                        PrimeraVez = true,
                    };
                    if (repoUsuarios.Update(nuevo))
                    {
                        ViewBag.error = "Contraseña cambiada! Inicie Sesión";
                    }
                    else
                    {
                        ViewBag.error = "Error al cambiar contraseña";
                    }
                }
                else
                {
                    ViewBag.error = "Contraseñas no coinciden";
                }
            }
            else
            {
                ViewBag.error = "Contraseña o usuario incorrecto";
            }
            return View();
        }

        private UsuarioModel MapearUsu(Usuario unUsuario)
        {
            return new UsuarioModel
            {
                Cedula = unUsuario.Cedula,
                OldPassword = unUsuario.Password
            };
        }
    }
}
